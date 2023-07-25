using System;

namespace Furnituremarket.Domain.Model
{
    public class OrderItem
    {
        public int FurnitureId { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Material { get; set; }
        public string Image { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }

        public OrderItem(int furnitureId, string name, 
            string description, string color, string material, string image, int count, decimal price)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException("Count must be greater than zero.");
            FurnitureId = furnitureId;
            Name = name;
            Description = description;
            Color = color;
            Material = material;
            Image = image;
            Count = count;
            Price = price;
        }
    }
}
