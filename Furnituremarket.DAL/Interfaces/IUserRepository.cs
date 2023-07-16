using Furnituremarket.Domain.Model;
using System.Threading.Tasks;

namespace Furnituremarket.DAL.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByName(string name);
        Task<User> GetById(int id);
    }
}
