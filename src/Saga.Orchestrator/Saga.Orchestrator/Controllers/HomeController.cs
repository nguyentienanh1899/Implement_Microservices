﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Saga.Orchestrator.Controllers
{
    public class HomeController : ControllerBase
    {
        public IActionResult Index() 
        {
            return Redirect("~/swagger");
        }
    }
}
