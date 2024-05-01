using Microsoft.AspNetCore.Mvc;
using ST10372065.Models;

namespace ST10372065.Controllers
{
    public class ProductDisplayController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var products = productDisplayModel.SelectProducts();
            return View(products);
        }
    }
}
