using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ST10372065.Models;
using System.Data.SqlClient;

namespace ST10372065.Controllers
{
    public class testController : Controller
    {
        public static string con_String = "Server=tcp:cldvpart001-sql-server.database.windows.net,1433;Initial Catalog=cldvpart001-sql-DB;Persist Security Info=False;User ID=zack;Password=Teacupungold6;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

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
    }
}