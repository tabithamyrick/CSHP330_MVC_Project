using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniStructorDB;
using MiniStructorMVCApp.Models;
using MiniStructorBusiness;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace MiniStructorMVCApp.Controllers
{
    public class AccountController : Controller
    {
        //Authentication Routes
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            UserLogin model = new UserLogin();
            if (returnUrl != null)
            {
                model.returnUrl = returnUrl;
            }
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
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        IsPersistent = false,
                        IssuedUtc = DateTimeOffset.UtcNow,
                    };

                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        claimsPrincipal,
                        authProperties).Wait();

                    if(model.returnUrl != null)
                    {
                        return Redirect(model.returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    
                }
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult LogOff()
        {
            HttpContext.Session.Remove("User");

            HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Register()
        {
           UserRegestration model = new UserRegestration();
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

        [HttpGet]
        public IActionResult UserClasses()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userBusiness = new UserBusiness();
                var userList = userBusiness.GetAllUsers().Where(x => x.UserEmail == User.Identity.Name);
                var classList = userBusiness.GetClassesForUser(userList.FirstOrDefault().UserId);
                if (classList == null)
                {
                    List<Class> model = new List<Class>();
                   return View(model);
                }
                else
                {
                    return View(classList);
                }
            }
            else
            {
                return RedirectToAction("Login", new { returnUrl = HttpContext.Request.Path });
            }
        }
    }
}
