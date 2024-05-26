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

        [HttpGet]
        public IActionResult Transactions()
        {
            string userIDStr = HttpContext.Session.GetString("UserID");
            int userID;
            List<TransactionsModel> transactions = new List<TransactionsModel>();

            if (!string.IsNullOrEmpty(userIDStr) && int.TryParse(userIDStr, out userID))
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(con_String))
                    {
                        string sql = "SELECT userCartProduct.* FROM userCartProduct INNER JOIN Cart ON userCartProduct.cartID = Cart.cartID WHERE Cart.userID = @UserID";
                        using (SqlCommand cmd = new SqlCommand(sql, con))
                        {
                            cmd.Parameters.AddWithValue("@UserID", userID);
                            con.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                TransactionsModel transaction = new TransactionsModel
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    UserID = Convert.ToInt32(reader["userID"]),
                                    ProductID = Convert.ToInt32(reader["productID"]),
                                    Quantity = Convert.ToInt32(reader["quantity"])
                                };
                                transactions.Add(transaction);
                            }
                            con.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    return View("Error");
                }
            }
            else
            {
                // Handle the error when UserID is not a valid integer
                return RedirectToAction("Login", "Home");
            }

            return View(transactions);
        }
    }
}