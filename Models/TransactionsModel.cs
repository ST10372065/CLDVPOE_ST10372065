using Microsoft.AspNetCore.Mvc;

namespace ST10372065.Models
{
    public class TransactionsModel
    {
        public int TransactionID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
    }
}
