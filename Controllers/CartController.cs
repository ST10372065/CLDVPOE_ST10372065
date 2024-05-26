using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ST10372065.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ST10372065.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            List<CartModel> cart = GetCart();
            return View("~/Views/Home/Cart.cshtml", cart);
        }

        [HttpPost]
        public IActionResult Add(int productID, string productName, double productPrice)
        {
            var cart = GetCart();

            var cartItem = cart.Find(item => item.ProductID == productID);
            if (cartItem == null)
            {
                cart.Add(new CartModel { ProductID = productID, ProductName = productName, ProductPrice = (decimal)productPrice, Quantity = 1 });
            }
            else
            {
                cartItem.Quantity++;
            }

            SaveCart(cart);

            return RedirectToAction("Index", "Home");
        }

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

        private void SaveCart(List<CartModel> cart)
        {
            var cartJson = JsonConvert.SerializeObject(cart);
            HttpContext.Session.SetString("Cart", cartJson);
        }
    }
}