using System.Collections.Generic;

namespace Furnituremarket.Domain.ViewModels.Cart
{
    public class CartViewModel
    {
        public int OrderId { get; }

        public int TotalCount { get; set; }

        public decimal TotalPrice { get; set; }

        public CartViewModel(int orderId) 
        {
            OrderId = orderId;
            TotalCount = 0;
            TotalPrice = 0m;
        }

    }
}
