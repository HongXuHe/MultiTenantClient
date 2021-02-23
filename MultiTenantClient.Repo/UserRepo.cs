using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MultiTenantClient.Entities.Dtos;
using MultiTenantClient.Entities.Identity;
using MultiTenantClient.Repo.UOW;
using MultiTenantClient.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiTenantClient.Repo
{
    public class UserRepo : RepoBase<UserEntity>, IUserRepo
    {
        private readonly IMapper _mapper;
        private readonly ILogger<UserRepo> _logger;

        public UserRepo(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserRepo> logger)
            : base(unitOfWork, mapper, logger)
        {
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// get user by email
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public async Task<UserDto> GetUserByEmailAsync(string userEmail)
        {
            var userFromDb = await UnitOfWork.GetDbContext().UserEntities
                .Include(x => x.Roles).ThenInclude(y => y.RoleEntity)
                .Include(x => x.Permissions).ThenInclude(y => y.PermissionEntity)
                .SingleOrDefaultAsync(x => x.Email == userEmail);
            return _mapper.Map<UserDto>(userFromDb);
        }

        public async Task<bool> LogInAsync(string userEmail, string password)
        {
            var hashedPassword = HashStringHelper.CreateMD5(password);
            return await UnitOfWork.GetDbContext().UserEntities.AnyAsync(x => x.Email == userEmail && x.Password == hashedPassword);
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var userFromDb = await UnitOfWork.GetDbContext().UserEntities
                .Include(x => x.Roles).ThenInclude(y => y.RoleEntity)
                .Include(x => x.Permissions).ThenInclude(y => y.PermissionEntity)
                .SingleOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<UserDto>(userFromDb);
        }

        /// <summary>
        /// check user exists based on email
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public async Task<bool> UserExistByEmailAsync(string userEmail)
        {
            return await UnitOfWork.GetDbContext().UserEntities.AnyAsync(u => u.Email == userEmail.Trim());
        }
    }
}
