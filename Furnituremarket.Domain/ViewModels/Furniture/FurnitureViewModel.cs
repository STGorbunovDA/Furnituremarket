using Microsoft.AspNetCore.Http;
using System;

namespace Furnituremarket.Domain.ViewModels.Furniture
{
    public class FurnitureViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Material { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreate { get; set; }
        public IFormFile Avatar { get; set; }
    }
}
