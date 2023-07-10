using Furnituremarket.Domain.Model;
using Furnituremarket.Domain.Response.Interfaces;
using Furnituremarket.Domain.ViewModels.Furniture;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Furnituremarket.Service.Interfaces
{
    public interface IFurnitureService
    {
        Task<IBaseResponse<bool>> CreateFurniture(
            FurnitureViewModel furnitureViewModel);

        Task<IBaseResponse<IEnumerable<Furniture>>> GetAllFurniture();

        Task<IBaseResponse<bool>> UpdateFurniture(int id,
            FurnitureViewModel furnitureViewModel);

        Task<IBaseResponse<bool>> DeleteFurniture(int id);

        Task<IBaseResponse<Furniture>> GetFurnitureById(int id);       

        Task<IBaseResponse<IEnumerable<Furniture>>> GetFurnitureByName(string name);
        
    }
}
