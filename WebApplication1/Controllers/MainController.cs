using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class MainController : Controller
    {
        private readonly ILogger<MainController> _logger;

        public MainController(ILogger<MainController> logger)
        {
            _logger = logger;
        }

        // Returns home view.
        public ActionResult Home()
        {
            return View();
        }

        // Returns about view.
        public ActionResult About()
        {
            return View();
        }

        // Returns contact view.
        public ActionResult Contact()
        {
            return View();
        }

        // Returns login view.
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult SearchBooks()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}