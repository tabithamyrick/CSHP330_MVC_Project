using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MiniStructorBusiness;
using MiniStructorDB;
using MiniStructorMVCApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace MiniStructorMVCApp.Controllers
{
    public class HomeController  : Controller
    {
      public IActionResult Index()
        {
            return View();
        }
    }
}
