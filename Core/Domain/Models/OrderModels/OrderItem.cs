namespace Domain.Models.OrderModels
{
    public class OrderItem :BaseEntity<Guid>
    {
        public OrderItem()
        {
        }
        public OrderItem(ProductInOrderItem Product, int quantity, decimal price)
        {
            Product = Product;
            Quantity = quantity;
            Price = price;
        }

        public  ProductInOrderItem Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
  
    }
}