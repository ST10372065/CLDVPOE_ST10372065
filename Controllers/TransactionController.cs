using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using ST10372065.Models;
using System.Diagnostics;
using Newtonsoft.Json;

namespace ST10372065.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TransactionsController(IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
            }

        public IActionResult Index()
        {
            return View();
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

        [HttpPost]
        public IActionResult PlaceOrder()
        {

            //int? userID = _httpContextAccessor.HttpContext?.Session?.GetInt32("UserID");

            //if (!userID.HasValue || userID.Value == 0)
            //{
            //    var errorModel = new ErrorViewModel
            //    {
            //        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            //        Message = "Invalid user ID.",
            //        Exception = new ArgumentNullException(nameof(userID))
            //    };
            //    return View("Error", errorModel);
            //}

            //try
            //{
            //    var cart = GetCart();

            //    TransactionsModel.processTransaction(userID.Value, cart);

            //    //using (SqlConnection con = new SqlConnection(productTable.con_String))
            //    //{
            //    //    string sql = "SELECT userID FROM Cart WHERE userID = @UserID";
            //    //    List<CartModel> cartItems = new List<CartModel>();

            //    //    using (SqlCommand cmd = new SqlCommand(sql, con))
            //    //    {
            //    //        cmd.Parameters.AddWithValue("@UserID", userID);
            //    //        con.Open();
            //    //        SqlDataReader reader = cmd.ExecuteReader();
            //    //        while (reader.Read())
            //    //        {
            //    //            CartModel item = new CartModel
            //    //            {
            //    //                ProductID = Convert.ToInt32(reader["productID"]),
            //    //                Quantity = Convert.ToInt32(reader["quantity"])
            //    //            };
            //    //            cartItems.Add(item);
            //    //        }
            //    //        con.Close();
            //    //    }

            //    //    foreach (var item in cartItems)
            //    //    {
            //    //        sql = "INSERT INTO transactionsTable (userID, productID, quantity) VALUES (@UserID, @ProductID, @Quantity)";
            //    //        using (SqlCommand cmd = new SqlCommand(sql, con))
            //    //        {
            //    //            cmd.Parameters.AddWithValue("@UserID", userID);
            //    //            cmd.Parameters.AddWithValue("@ProductID", item.ProductID);
            //    //            cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
            //    //            con.Open();
            //    //            cmd.ExecuteNonQuery();
            //    //            con.Close();
            //    //        }
            //    //    }
            //    //}

            //    return RedirectToAction("Transactions" , "Home");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    var errorModel = new ErrorViewModel
            //    {
            //        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            //        Message = ex.Message,
            //        Exception = ex
            //    };
            //   return View("Error", errorModel);
            return View("Error");
            //}
        }

        public IActionResult Transactions()
        {
            List<TransactionsModel> transactions = new List<TransactionsModel>();

            try
            {
                using (SqlConnection con = new SqlConnection(productTable.con_String))
                {
                    string sql = "SELECT TransactionID, userID, productID, quantity FROM transactionsTable";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            TransactionsModel transaction = new TransactionsModel
                            {
                                TransactionID = Convert.ToInt32(reader["TransactionID"]),
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

            return View(transactions);
        }
    }
}