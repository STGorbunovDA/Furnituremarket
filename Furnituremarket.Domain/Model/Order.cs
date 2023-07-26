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

        public int TotalCount => _items.Sum(item => item.Count);

        public decimal TotalPrice => _items.Sum(item => item.Count * item.Price);

        public Order(int id, IEnumerable<OrderItem> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));
            Id = id;
            _items = new List<OrderItem>(items);
        }

        public OrderItem GetItemFurniture(int FurnitureId)
        {
            int index = _items.FindIndex(item => item.FurnitureId == FurnitureId);
            if (index == -1)
                throw new InvalidOperationException("Furniture not found");

            return _items[index];
        }

        public void AddOrUpdateFurniture(Furniture furniture, int count)
        {
            if (furniture == null)
                throw new ArgumentNullException(nameof(furniture));

            int index = _items.FindIndex(item => item.FurnitureId == furniture.Id);

            if (index == -1)
                _items.Add(new OrderItem(furniture.Id,
                        furniture.Name, furniture.Description,
                        furniture.Color, furniture.Material, furniture.Image, count,
                        furniture.Price));

            else _items[index].Count += count;

            #region old

            //var item = _items.SingleOrDefault(x => x.FurnitureId == furniture.Id);

            //if (item == null)
            //    _items.Add(new OrderItem(furniture.Id, 
            //        furniture.Name,furniture.Description, 
            //        furniture.Color,furniture.Material, furniture.Image, count, 
            //        furniture.Price));
            //else
            //{
            //    _items.Remove(item);
            //    _items.Add(new OrderItem(furniture.Id, furniture.Name, furniture.Description,
            //        furniture.Color, furniture.Material, furniture.Image, 
            //        item.Count + count, furniture.Price));
            //}

            #endregion
        }

        public void RemoveFurniture(Furniture furniture)
        {
            if (furniture == null)
                throw new ArgumentNullException(nameof(furniture));
            int index = _items.FindIndex(item => item.FurnitureId == furniture.Id);
            if (index == -1)
                ThrowFurnitureException("Order does not contain specified item.", furniture);

            else _items.RemoveAt(index);
        }

        public void AddItemFurniture(Furniture furniture, int count)
        {
            if (furniture == null)
                throw new ArgumentNullException(nameof(furniture));

            var item = _items.SingleOrDefault(x => x.FurnitureId == furniture.Id);

            if (item == null)
                throw new ArgumentNullException(nameof(furniture));
            if (count > 1)
                item.Count = item.Count++;
        }


        public void RemoveItemFurniture(Furniture furniture, int count)
        {
            if (furniture == null)
                throw new ArgumentNullException(nameof(furniture));

            var item = _items.SingleOrDefault(x => x.FurnitureId == furniture.Id);

            if (item == null)
                throw new ArgumentNullException(nameof(furniture));

            if (count > 1)
                item.Count = item.Count--;

            else _items.Remove(item);
        }

       


        private void ThrowFurnitureException(string message, Furniture furniture) 
        { 
            var exception = new InvalidOperationException(message);
            exception.Data[nameof(furniture.Id)] = furniture.Id;
            exception.Data[nameof(furniture.Name)] = furniture.Name;
            exception.Data[nameof(furniture.Description)] = furniture.Description;
            throw exception;
        }
    }
}
