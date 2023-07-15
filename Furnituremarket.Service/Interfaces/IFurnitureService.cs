using Furnituremarket.Domain.Model;
using Furnituremarket.Domain.Response.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Furnituremarket.Service.Interfaces
{
    public interface IFurnitureService
    {
        Task<IBaseResponse<IEnumerable<Furniture>>> GetAllFurniture();
        Task<IBaseResponse<Furniture>> GetFurnitureById(int id);
        Task<IBaseResponse<IEnumerable<Furniture>>> GetFurniture(string query);


        Task<IBaseResponse<bool>> CreateFurniture(
            Furniture furnitureViewModel);

        Task<IBaseResponse<bool>> UpdateFurniture(int id,
            Furniture furnitureViewModel);
        Task<IBaseResponse<bool>> DeleteFurniture(int id);

        
        
    }
}
