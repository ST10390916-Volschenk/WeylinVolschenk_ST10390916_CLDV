using Microsoft.AspNetCore.Mvc;
using ST10390916_CLDV_POE.Models;
using System.Diagnostics;

namespace ST10390916_CLDV_POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public IActionResult _Layout()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
            ViewData["UserID"] = userID;
            return View();
        }

        public IActionResult Index()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
            ViewData["UserID"] = userID;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult AboutUs()
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
