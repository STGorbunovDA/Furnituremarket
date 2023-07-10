using System.Collections.Generic;
using System.Threading.Tasks;
using Furnituremarket.Domain.Model;

namespace Furnituremarket.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<bool> Create(T entity);
        Task<List<Furniture>> Read();
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);

    }
}
