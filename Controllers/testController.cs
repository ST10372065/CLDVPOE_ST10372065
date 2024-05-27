using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ST10372065.Models;
using System.Data.SqlClient;

namespace ST10372065.Controllers
{
    public class testController : Controller
    {
        public static string con_String = "Server=tcp:cldvpart001-sql-server.database.windows.net,1433;Initial Catalog=cldvpart001-sql-DB;Persist Security Info=False;User ID=zack;Password=Teacupungold6;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        /// <summary>
        /// gets products in cart list
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

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// takes usersID and adds product to cart
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// gets the users transactions from the database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Transactions()
        {
            string userIDStr = HttpContext.Session.GetString("UserID");
            int userID;
            List<TransactionsModel> transactions;

            if (!string.IsNullOrEmpty(userIDStr) && int.TryParse(userIDStr, out userID))
            {
                transactions = TransactionsModel.GetTransactionsByUserID(userID);
            }
            else
            {
                // Handle the error when UserID is not a valid integer
                return RedirectToAction("Login", "Home");
            }
            return View("~/Views/Home/PastTransactions.cshtml", transactions);
            //return View(transactions);
        }
        /// <summary>
        /// clears the cart for the user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ClearCart()
        {
            string userIDStr = HttpContext.Session.GetString("UserID");
            int userID;

            if (!string.IsNullOrEmpty(userIDStr) && int.TryParse(userIDStr, out userID))
            {
                var emptyCart = new List<CartModel>();
                HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(emptyCart));
            }
            else
            {
                // If UserID is not a valid integer or is not present in the session, redirect to the Login view
                return RedirectToAction("Login", "Home");
            }

         
            return RedirectToAction("Index", "Home");
        }
    }
}