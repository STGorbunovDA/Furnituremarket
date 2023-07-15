using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Furnituremarket.Domain.Model
{
    public class Furniture
    {
        public int Id { get; set; }

        [Display(Name = "Наименование")]
        [Required(ErrorMessage = "Введите наименование")]
        [MinLength(2, ErrorMessage = "Минимальная длина должна быть больше двух символов")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [Required(ErrorMessage = "Введите описание изделия")]
        [MinLength(2, ErrorMessage = "Минимальная длина должна быть больше двух символов")]
        public string Description { get; set; }

        [Display(Name = "Цвет")]
        [Required(ErrorMessage = "Введите цвет")]
        [MinLength(2, ErrorMessage = "Минимальная длина должна быть больше двух символов")]
        public string Color { get; set; }

        [Display(Name = "Материал")]
        [Required(ErrorMessage = "Введите материал")]
        [MinLength(2, ErrorMessage = "Минимальная длина должна быть больше двух символов")]
        public string Material { get; set; }

        [Display(Name = "Стоимость")]
        [Required(ErrorMessage = "Укажите стоимость")]
        public decimal Price { get; set; }
        public DateTime DateCreate { get; set; }

        //public IFormFile Avatar { get; set; }

        public string Image { get; set; }
        //
        public Furniture(int id, string name, string description,
            string color, string material, decimal price, DateTime dateCreate, string image)
        {
            Id = id;
            Name = name;
            Description = description;
            Color = color;
            Material = material;
            Price = price;
            DateCreate = dateCreate;
            Image = image;
        }
        public Furniture() { }

        internal static bool IsPrice(string price)
        {
            if (price == null) return false;

            price = price.Replace(" ", "")
                         .Replace(",", ".");

            return Regex.IsMatch(price, @"^\d+(.\d{1,2})?$");
        }
    }
}
