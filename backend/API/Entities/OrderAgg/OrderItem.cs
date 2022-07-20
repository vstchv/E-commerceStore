namespace API.Entities.OrderAgg
{
    public class OrderItem
    {
        public int Id { get; set; }
        public OrderedItems ItemOrdered { get; set; }
        public long Price { get; set; }
        public int Quantity { get; set; }
    }
}
