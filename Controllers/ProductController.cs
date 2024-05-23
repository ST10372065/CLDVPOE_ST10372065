using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ST10372065.Models;

namespace ST10372065.Controllers
{
    public class ProductController : Controller
    {
        public productTable prodtbl = new productTable();

        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProductController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public ActionResult MyWork(productTable products)
        {
            var userID = HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Home"); // Redirect to login page if user is not logged in
            }

            var result2 = prodtbl.insert_product(products);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult MyWork()
        {
            var userID = _httpContextAccessor.HttpContext.Session.GetString("UserID");

            if (string.IsNullOrEmpty(userID))
            {
                return RedirectToAction("Login", "Home"); // Redirect to login page if user is not logged in
            }

            return View(prodtbl);
        }
    }
}
