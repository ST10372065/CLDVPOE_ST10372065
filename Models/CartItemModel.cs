using Microsoft.AspNetCore.Mvc;

namespace ST10372065.Models
{
    public class CartItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }

        public double TotalPrice
        {
            get
            {
                return ProductPrice * Quantity;
            }
        }
    }
}
