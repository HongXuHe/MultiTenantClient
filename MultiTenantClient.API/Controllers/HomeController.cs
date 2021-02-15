using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MultiTenantClient.Aop.AttributeAop;
using MultiTenantClient.Entities;
using MultiTenantClient.Repo;
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
        private readonly IRepoBase _repoBase;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public HomeController(IRepoBase repoBase, IMapper mapper, IMediator mediator)
        {
            _repoBase = repoBase;
            _mapper = mapper;
            _mediator = mediator;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            //_repoBase.FindById("ss");
            //var entity = new List<BaseEntity> { new BaseEntity() { Name = "matt" } };
            //var dto = _mapper.Map<List<BaseDto>>(entity);
            var dto = new BaseDto
            {
                Name = "matt"
            };
            var res = _mediator.Send(dto).GetAwaiter().GetResult();
            return new JsonResult("ss");
        }
    }
}
