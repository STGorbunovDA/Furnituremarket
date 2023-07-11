using Furnituremarket.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Furnituremarket.DAL.Interfaces
{
    public interface IFurnitureRepository : IBaseRepository<Furniture>
    {
        Task<List<Furniture>> GetByQuery(string query);
        Task<Furniture> GetById(int id);
    }
}
