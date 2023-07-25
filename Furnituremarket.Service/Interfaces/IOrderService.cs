using Furnituremarket.Domain.Model;
using Furnituremarket.Domain.Response.Interfaces;
using System.Threading.Tasks;

namespace Furnituremarket.Service.Interfaces
{
    public interface IOrderService
    {
        Task<IBaseResponse<Order>> GetByIdOrder(int idOrder);
        Task<IBaseResponse<Order>> CreateOrder();
        Task<IBaseResponse<Order>> UpdateOrder(int idOrder);
    }
}
