using Furnituremarket.DAL.Interfaces;
using Furnituremarket.Domain.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Furnituremarket.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private static readonly List<Order> _orders = new List<Order>();
        
        public async Task<Order> Create()
        {
            int nextId = _orders.Count + 1;
            var order = new Order(nextId, new OrderItem[0]);
            _orders.Add(order);
            
            return await Task.Run(() =>
            {
                return order;
            });
        }

        public async Task<Order> GetById(int id)
        {
            return await Task.Run(() =>
            {
               return _orders.Single(order => order.Id == id);
            });
        }

        public async Task<Order> Update(Order order)
        {
            return await Task.Run(() =>
            {
                return order;
            });
        }
    }
}
