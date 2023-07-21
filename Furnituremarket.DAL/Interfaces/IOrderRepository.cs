using Furnituremarket.Domain.Model;
using System.Threading.Tasks;

namespace Furnituremarket.DAL.Interfaces
{
    public interface IOrderRepository 
    {
        Task<Order> Create();
        Task<Order> GetById(int id);
        Task<Order> Update(Order order);
    }
}
