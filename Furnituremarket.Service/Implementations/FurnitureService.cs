using Furnituremarket.DAL.Interfaces;
using Furnituremarket.Domain.Enum;
using Furnituremarket.Domain.Model;
using Furnituremarket.Domain.Response.Interfaces;
using Furnituremarket.Domain.Response;
using Furnituremarket.Domain.ViewModels.Furniture;
using Furnituremarket.Service.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Furnituremarket.Service.Implementations
{
    public class FurnitureService : IFurnitureService
    {
        private readonly IFurnitureRepository _furnitureRepository;

        public FurnitureService(IFurnitureRepository furnitureRepository)
        {
            _furnitureRepository = furnitureRepository;
        }

        public async Task<IBaseResponse<IEnumerable<Furniture>>> GetAllFurniture()
        {
            var baseResponse = new BaseResponse<IEnumerable<Furniture>>();

            try
            {
                var furnitureFull = await _furnitureRepository.Read();

                if (furnitureFull.Count == 0)
                {
                    baseResponse.Description = $"Furniture not found";
                    baseResponse.CodeStatus = StatusCode.FurnitureNotFound;
                    return baseResponse;
                }

                baseResponse.Data = furnitureFull;
                baseResponse.CodeStatus = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Furniture>>()
                {
                    Description = $"[GetAllFurniture] : {ex.Message}",
                    CodeStatus = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Furniture>> GetFurnitureById(int id)
        {
            var baseResponse = new BaseResponse<Furniture>();

            Furniture furniture = null;

            try
            {
                if (id > 0)
                    furniture = await _furnitureRepository.GetById(id);

                if (furniture == null || furniture.Name == null)
                {
                    baseResponse.Description = $"Furniture not found";
                    baseResponse.CodeStatus = StatusCode.FurnitureNotFound;
                    return baseResponse;
                }

                baseResponse.Data = furniture;
                baseResponse.CodeStatus = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Furniture>()
                {
                    Description = $"[GetFurniture] : {ex.Message}",
                    CodeStatus = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Furniture>>> GetFurniture(string query)
        {
            var baseResponse = new BaseResponse<IEnumerable<Furniture>>();

            List<Furniture> furnitureList = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(query))
                    furnitureList = await _furnitureRepository.GetByQuery(query);

                if (furnitureList == null)
                {
                    baseResponse.Description = $"Furniture not found";
                    baseResponse.CodeStatus = StatusCode.FurnitureNotFound;
                    return baseResponse;
                }

                baseResponse.Data = furnitureList;
                baseResponse.CodeStatus = StatusCode.OK;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Furniture>>()
                {
                    Description = $"[GetFurnitureByName] : {ex.Message}",
                    CodeStatus = StatusCode.InternalServerError
                };
            }
        }




        public async Task<IBaseResponse<bool>> CreateFurniture(
            FurnitureViewModel viewModel)
        {
            var baseResponse = new BaseResponse<bool>();

            try
            {
                var furniture = new Furniture()
                {
                    Name = viewModel.Name,
                    Description = viewModel.Description,
                    Color = viewModel.Color,
                    Material = viewModel.Material,
                    Price = viewModel.Price,
                    DateCreate = viewModel.DateCreate

                };

                baseResponse.Data = await _furnitureRepository.Create(furniture);

                if (baseResponse.Data == true)
                    baseResponse.CodeStatus = StatusCode.OK;
                else baseResponse.CodeStatus = StatusCode.CreateNotFound;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[CreateFurniture] : {ex.Message}",
                    CodeStatus = StatusCode.InternalServerError
                };
            }
        }   

        public async Task<IBaseResponse<bool>> UpdateFurniture(int id,
            FurnitureViewModel viewModel)
        {
            var baseResponse = new BaseResponse<bool>();

            Furniture furniture = null;

            try
            {
                if (id > 0)
                    furniture = await _furnitureRepository.GetById(id);

                if (furniture == null || furniture.Name == null)
                {
                    baseResponse.Description = $"Furniture not found";
                    baseResponse.CodeStatus = StatusCode.FurnitureNotFound;
                    return baseResponse;
                }

                furniture.Name = viewModel.Name;
                furniture.Description = viewModel.Description;
                furniture.Color = viewModel.Color;
                furniture.Material = viewModel.Material;
                furniture.Price = viewModel.Price;
                furniture.DateCreate = viewModel.DateCreate;

                baseResponse.Data = await _furnitureRepository.Update(furniture);

                if (baseResponse.Data == true)
                    baseResponse.CodeStatus = StatusCode.OK;
                else baseResponse.CodeStatus = StatusCode.UpdateNotFound;

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[UpdateFurniture] : {ex.Message}",
                    CodeStatus = StatusCode.InternalServerError
                };
            }

        } 

        public async Task<IBaseResponse<bool>> DeleteFurniture(int id)
        {
            var baseResponse = new BaseResponse<bool>();

            Furniture furniture = null;

            try
            {
                if (id > 0)
                    furniture = await _furnitureRepository.GetById(id);

                if (furniture == null)
                {
                    baseResponse.Description = $"Furniture not found";
                    baseResponse.CodeStatus = StatusCode.FurnitureNotFound;
                    return baseResponse;
                }

                baseResponse.Data = await _furnitureRepository.Delete(furniture);

                if (baseResponse.Data == true)
                    baseResponse.CodeStatus = StatusCode.OK;
                else baseResponse.CodeStatus = StatusCode.DeleteNotFound;

                return baseResponse;

            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteFurniture] : {ex.Message}",
                    CodeStatus = StatusCode.InternalServerError
                };
            }
        }

       

        

    }
}
