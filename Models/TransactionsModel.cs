using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ST10372065.Models
{
    public class TransactionsModel
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public static String con_String = "Server=tcp:cldvpart001-sql-server.database.windows.net,1433;Initial Catalog=cldvpart001-sql-DB;Persist Security Info=False;User ID=zack;Password=Teacupungold6;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

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
    }
}