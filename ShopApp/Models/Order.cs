using System.Collections.Generic;

namespace ShopApp.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public List<CartItem> Items { get; set; }
        public decimal TotalAmount { get; set; }
    }
}