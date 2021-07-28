using Microsoft.AspNetCore.Mvc;
using MiniStructorMVCApp.Models;
using System.Diagnostics;


namespace MiniStructorMVCApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
