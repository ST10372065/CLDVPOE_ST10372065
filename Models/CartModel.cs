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

        public static int AddToCart(int userID, int productID, int quantity)
        {
            int newEntryID = 0;

            using (SqlConnection con = new SqlConnection(con_String))
            {
                string sql = "INSERT INTO Cart (userID) OUTPUT Inserted.cartID VALUES (@UserID);";
                string sql2 = "INSERT INTO userCartProducts (productID, quantity, cartID) VALUES (@ProductID, @Quantity, @cartID)";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    
                    var cartID = cmd.ExecuteNonQueryAsync();
                    //cmd.Parameters.AddWithValue("@UserID", userID);
                    //cmd.Parameters.AddWithValue("@ProductID", productID);
                    //cmd.Parameters.AddWithValue("@Quantity", quantity);

                    con.Open();
                    newEntryID = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return newEntryID;
        }

        //public static List<CartModel> GetCartItems(int userID)
        //{
        //    List<CartModel> cartItems = new List<CartModel>();

        //    using (SqlConnection con = new SqlConnection(con_String))
        //    {
        //        string sql = "SELECT Cart.productID, productTable.Name AS productName, productTable.Price AS productPrice, Cart.quantity " +
        //                     "FROM Cart " +
        //                     "JOIN productTable ON Cart.productID = productTable.ID " +
        //                     "WHERE Cart.userID = @UserID";

        //        using (SqlCommand cmd = new SqlCommand(sql, con))
        //        {
        //            cmd.Parameters.AddWithValue("@UserID", userID);

        //            con.Open();
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            while (reader.Read())
        //            {
        //                CartModel item = new CartModel
        //                {
        //                    ProductID = Convert.ToInt32(reader["productID"]),
        //                    ProductName = reader["productName"].ToString(),
        //                    ProductPrice = Convert.ToDecimal(reader["productPrice"]),
        //                    Quantity = Convert.ToInt32(reader["quantity"])
        //                };
        //                cartItems.Add(item);
        //            }
        //        }
        //    }

        //    return cartItems;
        //}

        //public static void ClearCart(int userID)
        //{
        //    using (SqlConnection con = new SqlConnection(con_String))
        //    {
        //        string sql = "DELETE FROM Cart WHERE userID = @UserID";

        //        using (SqlCommand cmd = new SqlCommand(sql, con))
        //        {
        //            cmd.Parameters.AddWithValue("@UserID", userID);


        //            con.Open();
        //            cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
    }
}