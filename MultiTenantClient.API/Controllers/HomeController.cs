using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        public HomeController( IMapper mapper, IMediator mediator, IUserRepo userRepo)
        {
            _mapper = mapper;
            _mediator = mediator;
            _userRepo = userRepo;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            //_repoBase.FindById("ss");
            //var entity = new List<BaseEntity> { new BaseEntity() { Name = "matt" } };
            var dto = _mapper.Map<List<UserDto>>(_userRepo.GetEntities(x => true).ToList());
            
          //  var dto = _userRepo.GetEntities(x=>true).ToList();
          //  var res = _mediator.Send(dto).GetAwaiter().GetResult();
            return new JsonResult("ss");
        }
    }
}
