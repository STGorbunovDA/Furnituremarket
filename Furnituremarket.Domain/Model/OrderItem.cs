using System;

namespace Furnituremarket.Domain.Model
{
    public class OrderItem
    {
        public int FurnitureId { get; }
        public int Count { get; }
        public decimal Price { get; }

        public OrderItem(int furnitureId, int count, decimal price)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("Count must be greater than zero.");
            FurnitureId = furnitureId;
            Count = count;
            Price = price;
        }
    }
}
