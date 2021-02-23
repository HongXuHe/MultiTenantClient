using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MultiTenantClient.Entities.Dtos;
using MultiTenantClient.Entities.Identity;
using MultiTenantClient.Repo;
using MultiTenantClient.Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantClient.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        #region ctor and props
        private readonly ILogger<AccountController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IUserRepo _userRepo;
        private readonly IRoleRepo _roleRepo;
        private readonly IPermissionRepo _permissionRepo;
        private readonly IMapper _mapper;

        public AccountController(IUserRepo userRepo,
            IRoleRepo roleRepo,
            IPermissionRepo permissionRepo,
            IMapper mapper,
            ILogger<AccountController> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            _userRepo = userRepo;
            _roleRepo = roleRepo;
            _permissionRepo = permissionRepo;
            _mapper = mapper;
        } 
        #endregion

        [HttpGet("UserList")]
        public async Task<IActionResult> UserList(int pageIndex = 1, int pageSize = 5)
        {
            var context = _userRepo.UnitOfWork.GetDbContext();
            var dtos = _mapper.Map<List<UserDto>>(await context.UserEntities
                .Include(x => x.Roles).ThenInclude(y => y.RoleEntity)
                .Include(x => x.Permissions).ThenInclude(y => y.PermissionEntity)
                .Skip((pageIndex - 1) * pageSize > 0 ? (pageIndex - 1) * pageSize : 0)
                .Take(pageSize)
                .ToListAsync());

            // CreateToken(new UserDto { Email = "matt@gmail.com", Roles = new List<string> { "Admin" } });
            if (dtos != null)
            {
                return Ok(dtos);
            }
            return new JsonResult("No Users Available");
        }

        [HttpPost]
        public async Task<IActionResult> Register(CreateUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userExist = await _userRepo.UserExistByEmailAsync(userDto.Email);
            if (userExist)
            {
                return BadRequest("User Already Exists");
            }
            userDto.Password = HashStringHelper.CreateMD5(userDto.Password);
            var userEntity = _mapper.Map<UserEntity>(userDto);
            foreach (var role in userDto.RoleNames)
            {
                var roleEntity = await _roleRepo.GetEntities(r => r.Name == role).SingleOrDefaultAsync();
                if (roleEntity != null)
                {
                    userEntity.Roles.Add(new User_Role
                    {
                        RoleEntity = roleEntity
                    });
                }
            }
            foreach (var perm in userDto.PermissionNames)
            {
                var permEntity = await _permissionRepo.GetEntities(r => r.PermissionName == perm).SingleOrDefaultAsync();
                if (permEntity != null)
                {
                    userEntity.Permissions.Add(new User_Permission
                    {
                        PermissionEntity = permEntity
                    });
                }
            }
            var success = await _userRepo.InsertAsync(userEntity);
            if (success > 0)
            {
                return RedirectToAction(nameof(UserList));
            }
            throw new Exception($"Insert user {userEntity.Id} error");
        }

        [HttpGet("Users/Id/{Id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            var userFromDb = await _userRepo.GetUserByIdAsync(id);
            if (userFromDb != null)
            {
                return Ok(_mapper.Map<UserDto>(userFromDb));
            }
            return BadRequest("Cannot find user");
        }

        [HttpGet("Users/Email/{Email}")]
        public async Task<ActionResult<UserDto>> GetUserById(string Email)
        {
            var userFromDb = await _userRepo.GetUserByEmailAsync(Email);
            if (userFromDb != null)
            {
                return Ok(_mapper.Map<UserDto>(userFromDb));
            }
            return BadRequest("Cannot find user");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LogIn(UserLogInDto logInDto)
        {
            var response = new ResponseDto<string>();
            var result = await _userRepo.LogInAsync(logInDto.Email, logInDto.Password);
            //username and password correct
            if (result)
            {
                var user = await _userRepo.GetUserByEmailAsync(logInDto.Email);
                var token = CreateToken(user);
                response.Success = true;
                response.Data = token;
                return Ok(token);
            }
            response.Success = false;
            response.Message = "UserEmail or password incorrect";
            return NotFound(response);
        }

        //todo eidtuser deleteuser

        #region Private Methods
        private string CreateToken(UserDto userDto)
        {
            List<string> claimList = userDto.Permissions;
            foreach (var role in userDto.Roles)
            {
                if (_roleRepo.ExistAsync(x => x.Name == role).GetAwaiter().GetResult())
                {
                    var permissionList = _roleRepo.UnitOfWork.GetDbContext().RoleEntities.Include(x => x.Permissions)
                          .ThenInclude(p => p.PermissionEntity)
                          .Where(x => x.Name == role).SelectMany(y => y.Permissions.Select(z => z.PermissionEntity.PermissionName)).ToList();
                    foreach (var perm in permissionList)
                    {
                        if (!claimList.Contains(perm))
                        {
                            claimList.Add(perm);
                        }
                    }
                }
            }
            List<Claim> claims = new List<Claim>();
            foreach (var claim in claimList)
            {
                claims.Add(new Claim(claim, claim));
            }
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
        .GetBytes(_configuration.GetSection("MultiTenantClient:tokenKey").Value));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            var returnStr = tokenHandler.WriteToken(token);
            return returnStr;
        }
        #endregion
    }
}
