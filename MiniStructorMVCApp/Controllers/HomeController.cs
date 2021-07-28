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
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //Authentication Routes
        [HttpGet]
        public IActionResult Login()
        {
            UserLogin model = new UserLogin();
            return View();
        }

        [HttpPost]
        public IActionResult Login(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var userBusiness = new UserBusiness();
                var user = userBusiness.LogIn(model.UserEmail, model.Password); 

                if (user == null)
                {
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    var json = JsonConvert.SerializeObject(new MiniStructorBusiness.UserModel
                    {
                        Id = user.Id,
                        Name = user.Name
                    });

                    HttpContext.Session.SetString("User", json);

                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, "User"),
                };

                    var claimsIdentity = new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    var authProperties = new AuthenticationProperties
                    {
                        AllowRefresh = false,
                        // Refreshing the authentication session should be allowed.

                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        // The time at which the authentication ticket expires. A 
                        // value set here overrides the ExpireTimeSpan option of 
                        // CookieAuthenticationOptions set with AddCookie.

                        IsPersistent = false,
                        // Whether the authentication session is persisted across 
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session-based.

                        IssuedUtc = DateTimeOffset.UtcNow,
                        // The time at which the authentication ticket was issued.
                    };

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrincipal,
                        authProperties).Wait();

                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult LogOff()
        {
            HttpContext.Session.Remove("User");

            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

            return Redirect("Index");
        }
        [HttpGet]
        public IActionResult Register()
        {
            User model = new User();
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                var userBusiness = new UserBusiness();
                var user = userBusiness.Register(model);

                if (user == null)
                {
                    ModelState.AddModelError("", "Something Went Wrong, Please Try Again");
                }
                else
                {
                    ViewBag.msg = "Registration Successful. Please Login.";
                }
                return RedirectToAction("Login");
            }
            ViewBag.msg = "Something Went Wrong, Please Try Again.";
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

      
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}
