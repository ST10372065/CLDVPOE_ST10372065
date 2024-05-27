using ST10372065.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ST10372065.Models
{
    public class CartModel
    {
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }

        public static String con_String = "Server=tcp:cldvpart001-sql-server.database.windows.net,1433;Initial Catalog=cldvpart001-sql-DB;Persist Security Info=False;User ID=zack;Password=Teacupungold6;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";
        /// <summary>
        /// uses an SQL connection to add a product to the cart. The SQL command is to INSERT into the datbase.
        /// https://www.w3schools.com/sql/sql_insert.asp
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="productID"></param>
        /// <param name="quantity"></param>
        /// <returns></returns>
        public static int AddToCart(int userID, int productID, int quantity)
        {
            int newEntryID = 0;

            using (SqlConnection con = new SqlConnection(con_String))
            {
                string sql = "INSERT INTO Cart (userID) OUTPUT Inserted.cartID VALUES (@UserID);";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    
                    var cartID = cmd.ExecuteNonQueryAsync();
                    con.Open();
                    newEntryID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return newEntryID;
        }
    }
}