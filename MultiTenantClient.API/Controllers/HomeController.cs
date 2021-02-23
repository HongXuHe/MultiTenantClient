using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantClient.Aop.AttributeAop;
using MultiTenantClient.Entities;
using MultiTenantClient.Entities.Dtos;
using MultiTenantClient.Repo;
using MultiTenantClient.Repo.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantClient.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IUserRepo _userRepo;

        public HomeController(IMapper mapper, IMediator mediator, IUserRepo userRepo)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userRepo = userRepo;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            var context = _userRepo.UnitOfWork.GetDbContext();
            var dtos =_mapper.Map<List<UserDto>>(context.UserEntities
                .Include(x=>x.Roles).ThenInclude(y=>y.RoleEntity)
                .Include(x=>x.Permissions).ThenInclude(y=>y.PermissionEntity)
                .ToList());
            return new JsonResult(dtos);
        }
    }
}
