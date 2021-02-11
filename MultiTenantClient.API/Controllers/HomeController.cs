﻿using Microsoft.AspNetCore.Mvc;
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
        [HttpGet("")]
        public IActionResult Index()
        {
            return new JsonResult("ss");
        }
    }
}
