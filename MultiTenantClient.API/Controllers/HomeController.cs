using Microsoft.AspNetCore.Mvc;
using MultiTenantClient.Aop.AttributeAop;
using MultiTenantClient.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantClient.API.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController:ControllerBase
    {
        private readonly IRepoBase _repoBase;

        public HomeController(IRepoBase repoBase)
        {
            _repoBase = repoBase;
        }
        [HttpGet("")]
        public IActionResult Index()
        {
            _repoBase.FindById("ss");
            return new JsonResult("ss");
        }
    }
}
