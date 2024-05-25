using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using ST10372065.Models;

namespace ST10372065.Controllers
{
    public class TransactionsController : Controller
    {
        [HttpPost]
        public IActionResult PlaceOrder(int userID, int productID, int quantity)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(productTable.con_String))
                {
                    string sql = "INSERT INTO transactionTable (userID, productID, quantity) VALUES (@UserID, @ProductID, @Quantity)";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@UserID", userID);
                        cmd.Parameters.AddWithValue("@ProductID", productID);
                        cmd.Parameters.AddWithValue("@Quantity", quantity);

                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }

                // Redirect to the Transactions view after placing the order
                return RedirectToAction("Transactions", "Transactions");
            }
            catch (Exception ex)
            {
                // Handle any errors
                return View("Error");
            }
        }

        public IActionResult Transactions()
        {
            List<TransactionsModel> transactions = new List<TransactionsModel>();

            try
            {
                using (SqlConnection con = new SqlConnection("YourConnectionString"))
                {
                    string sql = "SELECT TransactionID, userID, productID, quantity FROM transactiontable";
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