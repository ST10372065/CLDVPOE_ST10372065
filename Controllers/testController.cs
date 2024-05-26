using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json;
using ST10372065.Models;

namespace ST10372065.Controllers
{
    public class testController : Controller
    {
        private List<CartModel> GetCart()
        {
            var cartJson = HttpContext.Session.GetString("Cart");
            if (cartJson == null)
            {
                return new List<CartModel>();
            }
            else
            {
                return JsonConvert.DeserializeObject<List<CartModel>>(cartJson);
            }
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PlaceOrder()
        {
            string userIDStr = HttpContext.Session.GetString("UserID");
            int userID;
            if (!string.IsNullOrEmpty(userIDStr) && int.TryParse(userIDStr, out userID))
            {
                var cart = GetCart();
                TransactionsModel.processTransaction(userID, cart);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Handle the error when UserID is not a valid integer
                return RedirectToAction("Login", "Home");
            }
        }
    }
}
