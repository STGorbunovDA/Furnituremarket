using System;
using System.Collections.Generic;
using System.Linq;

namespace Furnituremarket.Domain.Model
{
    public class Order
    {
        public int Id { get; }

        private List<OrderItem> _items;
        public IReadOnlyCollection<OrderItem> Items
        {
            get { return _items; }
        }

        public int TotalCount
        {
            get { return _items.Sum(item => item.Count); }
        }

        public decimal TotalPrice
        {
            get { return _items.Sum(item => item.Count * item.Price); }
        }

        public Order(int id, IEnumerable<OrderItem> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            Id = id;
            _items = new List<OrderItem>(items);
        }

        public void AddItemFurniture(Furniture furniture, int count)
        {
            if(furniture == null)
                throw new ArgumentNullException(nameof(furniture));

            var item = _items.SingleOrDefault(x => x.FurnitureId == furniture.Id);

            if (item == null)
                _items.Add(new OrderItem(furniture.Id, 
                    furniture.Name,furniture.Description, 
                    furniture.Color,furniture.Material, furniture.Image, count, 
                    furniture.Price));
            else
            {
                _items.Remove(item);
                _items.Add(new OrderItem(furniture.Id, furniture.Name, furniture.Description,
                    furniture.Color, furniture.Material, furniture.Image, 
                    item.Count + count, furniture.Price));
            }
        }
    }
}
