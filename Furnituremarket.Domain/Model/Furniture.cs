﻿using System;

namespace Furnituremarket.Domain.Model
{
    public class Furniture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public string Material { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreate { get; set; }

        public Furniture(int id, string name, string description,
            string color, string material, decimal price, DateTime dateCreate)
        {
            Id = id;
            Name = name;
            Description = description;
            Color = color;
            Material = material;
            Price = price;
            DateCreate = dateCreate;
        }
        public Furniture() { }
    }
}
