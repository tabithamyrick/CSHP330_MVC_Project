using Microsoft.AspNetCore.Mvc;


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
