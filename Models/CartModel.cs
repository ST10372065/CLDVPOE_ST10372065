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

        public static string con_string = productTable.con_String;

        public static void AddToCart(int userID, int productID, int quantity)
        {
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "IF EXISTS (SELECT * FROM Cart WHERE userID = @UserID AND productID = @ProductID) " +
                             "UPDATE Cart SET quantity = quantity + @Quantity WHERE userID = @UserID AND productID = @ProductID " +
                             "ELSE INSERT INTO Cart (userID, productID, quantity) VALUES (@UserID, @ProductID, @Quantity)";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@ProductID", productID);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static List<CartModel> GetCartItems(int userID)
        {
            List<CartModel> cartItems = new List<CartModel>();

            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT Cart.productID, productTable.Name AS productName, productTable.Price AS productPrice, Cart.quantity " +
                             "FROM Cart " +
                             "JOIN productTable ON Cart.productID = productTable.ID " +
                             "WHERE Cart.userID = @UserID";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        CartModel item = new CartModel
                        {
                            ProductID = Convert.ToInt32(reader["productID"]),
                            ProductName = reader["productName"].ToString(),
                            ProductPrice = Convert.ToDecimal(reader["productPrice"]),
                            Quantity = Convert.ToInt32(reader["quantity"])
                        };
                        cartItems.Add(item);
                    }
                }
            }

            return cartItems;
        }

        public static void ClearCart(int userID)
        {
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "DELETE FROM Cart WHERE userID = @UserID";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);


                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}