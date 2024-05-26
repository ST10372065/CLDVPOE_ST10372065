using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ST10372065.Models;
using System.Diagnostics;

namespace ST10372065.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor; // Add IHttpContextAccessor

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor; // Initialize IHttpContextAccessor
        }

        public IActionResult Index()
        {
            // Retrieve all products from the database
            List<productTable> products = productTable.GetAllProducts();

            // Pass products and userID to the view
            ViewData["Products"] = products;
            return View();
        }

        public IActionResult AboutUS()
        {
            return View();
        }

        public IActionResult ContactUS()
        {
            return View();
        }

        public IActionResult MyWork()
        {
            int? userID = _httpContextAccessor.HttpContext.Session.GetInt32("UserID");
            ViewData["UserID"] = userID;
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Clear the session
            return RedirectToAction("Index"); // Redirect to the index page
        }

        public IActionResult Cart()
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
