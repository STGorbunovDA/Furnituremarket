using System.Collections.Generic;

namespace Furnituremarket.Domain.ViewModels.Order
{
    public class OrderViewModel
    {
        public int OrderId { get; }

        public int TotalCount { get; set; }

        public decimal TotalPrice { get; set; }

        public OrderViewModel(int orderId)
        {
            OrderId = orderId;
            TotalCount = 0;
            TotalPrice = 0m;
        }

    }
}
