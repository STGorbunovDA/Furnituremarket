using System;

namespace Furnituremarket.Domain.Model
{
    public class OrderItem
    {
        public int FurnitureId { get; }
        public string Name { get; }
        public string Description { get; }
        public string Color { get; }
        public string Material { get; }
        public string Image { get; }

        private int count;
        public int Count
        {
            get { return count; }
            set
            {
                ThrowIfInvalidCount(value);
                count = value;
            }
        }
        public decimal Price { get; }

        public OrderItem(int furnitureId, string name,
            string description, string color, string material, string image, int count, decimal price)
        {
            ThrowIfInvalidCount(count);

            FurnitureId = furnitureId;
            Name = name;
            Description = description;
            Color = color;
            Material = material;
            Image = image;
            Count = count;
            Price = price;
        }

        private static void ThrowIfInvalidCount(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("Count must be greater than zero.");
        }
    }
}
