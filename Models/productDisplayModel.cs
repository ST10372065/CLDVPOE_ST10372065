using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ST10372065.Models
{
    public class productDisplayModel
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public bool ProductAvailability { get; set; }

        public productDisplayModel() { }

        //Parameterized Constructor: This constructor takes five parameters (id, name, price, availability) and initializes the corresponding properties of ProductDisplayModel with the provided values.
        public productDisplayModel(int id, string name, double price, bool availability)
        {
            ProductID = id;
            ProductName = name;
            ProductPrice = price;
            ProductAvailability = availability;
        }

        public static List<productDisplayModel> SelectProducts()
        {
            List<productDisplayModel> products = new List<productDisplayModel>();

            string con_string = "Integrated Security=SSPI;Persist Security Info=False;User ID=\"\";Initial Catalog=test;Data Source=labVMH8OX\\SQLEXPRESS";
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT ProductID, ProductName, ProductPrice, ProductAvailability FROM productTable";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    productDisplayModel product = new productDisplayModel();
                    product.ProductID = Convert.ToInt32(reader["ProductID"]);
                    product.ProductName = Convert.ToString(reader["ProductName"]);
                    product.ProductPrice = Convert.ToDouble(reader["ProductPrice"]);
                    product.ProductAvailability = Convert.ToBoolean(reader["ProductAvailability"]);
                    products.Add(product);
                }
                reader.Close();
            }
            return products;
        }
    }
}
