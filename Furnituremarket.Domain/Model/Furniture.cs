namespace Furnituremarket.Domain.Model
{
    public class Furniture
    {
        public int Id { get; set; }
        public string NameTitle { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Material { get; set; }
        public decimal Price { get; set; }

        public Furniture(int id, string nameTitle, string description,
            string color, string material, decimal price)
        {
            Id = id;
            NameTitle = nameTitle;
            Description = description;
            Color = color;
            Material = material;
            Price = price;
        }
    }
}
