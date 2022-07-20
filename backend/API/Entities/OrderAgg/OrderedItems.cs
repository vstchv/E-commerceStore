using API.Entities.OrderAggregate;

using Microsoft.EntityFrameworkCore;

namespace API.Entities.OrderAgg
{
    [Owned]
    public class OrderedItems
    {
        public int ProductId { get; set; }
        public ProductItemOrdered ItemOrdered { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
    }
}
