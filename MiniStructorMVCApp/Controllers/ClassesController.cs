using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniStructorDB;
using MiniStructorRepository;
using MiniStructorBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniStructorMVCApp.Controllers
{
    public class ClassesController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("ClassList");
        }

        [HttpGet]
        public IActionResult ClassList()
        {
            ClassBusiness classBusienss = new ClassBusiness();
            var classList = classBusienss.GetAllClasses();

            return View(classList);
        }

        [HttpGet]
        public IActionResult Register(int classID)
        {
            if (User.Identity.IsAuthenticated)
            {
                UserClassBusiness userClassBusiness = new UserClassBusiness();
                userClassBusiness.Register(classID, User.Identity.Name);

                return RedirectToAction("UserClasses", "Account");
            }
            else
            {
                var test = Url.ActionContext;
                return RedirectToAction("Login", "Account", new { returnUrl = "/Classes/ClassList"});
            }
        }
    }
}
