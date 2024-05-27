using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ST10372065.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ST10372065.Controllers
{
    public class CartController : Controller
    {
        /// <summary>
        /// create a cart list
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            List<CartModel> cart = GetCart();
            return View("~/Views/Home/Cart.cshtml", cart);
        }
        /// <summary>
        /// Handles the logic for adding items to a shopping cart.Checks if the item already exists in the cart and either adds a new item or increments the quantity of an existing item. 
        /// The updated cart is then saved and the user is redirected to the cart's index page.
        /// </summary>
        /// <param name="productID"></param>
        /// <param name="productName"></param>
        /// <param name="productPrice"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Add(int productID, string productName, double productPrice, int quantity)
        {
            var cart = GetCart();

            var cartItem = cart.Find(item => item.ProductID == productID);
            if (cartItem == null)
            {
                cart.Add(new CartModel { ProductID = productID, ProductName = productName, ProductPrice = (decimal)productPrice, Quantity = quantity });
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            SaveCart(cart);

            return RedirectToAction("Index", "Home");
        }
        /// <summary>
        /// Gets the cart from the session and returns it as a list of CartModel objects.
        /// </summary>
        /// <returns></returns>
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