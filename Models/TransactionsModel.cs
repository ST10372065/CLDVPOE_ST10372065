using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ST10372065.Models
{
    public class TransactionsModel
    {
        public int TransactionID { get; set; }
        public int CartID { get; set; }
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }


        public static String con_String = "Server=tcp:cldvpart001-sql-server.database.windows.net,1433;Initial Catalog=cldvpart001-sql-DB;Persist Security Info=False;User ID=zack;Password=Teacupungold6;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

        public TransactionsModel(int iD, int cartID, int productID,int quantity) 
        { 
            ID = iD;
            CartID = cartID;
            ProductID = productID;
            Quantity = quantity;
            ProductName = GetProductName(productID);
        }
        public static int processTransaction(int userID, List<CartModel> cart)
        {
            int newEntryID = 0;
            int cartID = 999;

            using (SqlConnection con = new SqlConnection(con_String))
            {
                string sql = "INSERT INTO Cart (userID) OUTPUT Inserted.cartID VALUES (@UserID)";
                string sql2 = "INSERT INTO userCartProduct (cartID,productID,quantity) VALUES (@CartID, @ProductID, @Quantity)";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    newEntryID = Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();
                }

                using (SqlCommand cmd = new SqlCommand(sql2, con))
                {
                    con.Open();
                    foreach (var item in cart)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@CartID", newEntryID);
                        cmd.Parameters.AddWithValue("@ProductID", item.ProductID);
                        cmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                        cmd.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }

            return newEntryID;
        }

        public static List<TransactionsModel> GetTransactionsByUserID(int userID)
        {
            List<TransactionsModel> transactions = new List<TransactionsModel>();

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
                            TransactionsModel transaction = new TransactionsModel(iD : Convert.ToInt32(reader["ID"]), cartID : Convert.ToInt32(reader["cartID"]), productID : Convert.ToInt32(reader["productID"]), quantity : Convert.ToInt32(reader["quantity"]));
                            
                               
                            transactions.Add(transaction);
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors
                // You might want to log the exception or throw it to be handled by a higher level method
               
            }

            return transactions;
        }

        private static string GetProductName(int productID)
        {
            string productName;
            using (SqlConnection con = new SqlConnection(con_String))
            {
                string sql = "SELECT productName FROM productTable WHERE productID = @ProductID";
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@ProductID", productID);
                    con.Open();
                    productName = Convert.ToString(cmd.ExecuteScalar());
                    con.Close();
                }
            }
            return productName;
        }
    }
}