using Furnituremarket.DAL.Interfaces;
using Furnituremarket.Domain.Enum;
using Furnituremarket.Domain.Model;
using Furnituremarket.Domain.Response.Interfaces;
using Furnituremarket.Domain.Response;
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
            try
            {
                var furnitureFull = await _furnitureRepository.Read();

                if (furnitureFull.Count == 0)
                {
                    return new BaseResponse<IEnumerable<Furniture>>()
                    {
                        Data = furnitureFull,
                        Description = $"Furniture not found",
                        CodeStatus = StatusCode.FurnitureNotFound
                    };
                }

                return new BaseResponse<IEnumerable<Furniture>>()
                {
                    Data = furnitureFull,
                    CodeStatus = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Furniture>>()
                {
                    Description = "[GetAllFurniture] ||: " + ex + " ||: " + ex.Message,
                    CodeStatus = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Furniture>> GetFurnitureById(int id)
        {
            try
            {
                var furniture = await _furnitureRepository.GetById(id);

                if (furniture.Name == null)
                {
                    return new BaseResponse<Furniture>()
                    {
                        Description = $"Furniture not found",
                        CodeStatus = StatusCode.FurnitureNotFound
                    };
                }
                return new BaseResponse<Furniture>()
                {
                    Data = furniture,
                    CodeStatus = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Furniture>()
                {
                    Description = "[GetFurnitureById] ||: " + ex + " ||: " + ex.Message,
                    CodeStatus = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Furniture>>> GetFurniture(string query)
        {
            try
            {
                var furnitureList = await _furnitureRepository.GetByQuery(query);

                if (furnitureList.Count == 0)
                {
                    return new BaseResponse<IEnumerable<Furniture>>()
                    {
                        Data = furnitureList,
                        Description = $"Furniture not found",
                        CodeStatus = StatusCode.FurnitureNotFound
                    };
                }
                return new BaseResponse<IEnumerable<Furniture>>()
                {
                    Data = furnitureList,
                    CodeStatus = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Furniture>>()
                {
                    Description = "[GetByQuery] ||: " + ex + " ||: " + ex.Message,
                    CodeStatus = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> CreateFurniture(
            Furniture model)
        {
            try
            {
                var furniture = new Furniture()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Color = model.Color,
                    Material = model.Material,
                    Price = model.Price,
                    DateCreate = DateTime.Now,
                    Image = model.Image
                };

                if (await _furnitureRepository.Create(furniture))
                {
                    return new BaseResponse<bool>()
                    {
                        CodeStatus = StatusCode.OK
                    };
                }
                else
                {
                    return new BaseResponse<bool>()
                    {
                        CodeStatus = StatusCode.CreateNotFound
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = "[CreateFurniture] ||: " + ex + " ||: " + ex.Message,
                    CodeStatus = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> UpdateFurniture(int id,
            Furniture model)
        {
            try
            {
                var furniture = await _furnitureRepository.GetById(id);

                if (furniture.Name == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = $"Furniture not found",
                        CodeStatus = StatusCode.FurnitureNotFound
                    };
                }

                furniture = new Furniture()
                {
                    Id = id,
                    Name = model.Name,
                    Description = model.Description,
                    Color = model.Color,
                    Material = model.Material,
                    Price = model.Price,
                    DateCreate = DateTime.Now,
                    Image = model.Image
                };

                if (await _furnitureRepository.Update(furniture))
                {
                    return new BaseResponse<bool>()
                    {
                        CodeStatus = StatusCode.OK
                    };
                }
                else
                {
                    return new BaseResponse<bool>()
                    {
                        CodeStatus = StatusCode.UpdateNotFound
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = "[UpdateFurniture] ||: " + ex + " ||: " + ex.Message,
                    CodeStatus = StatusCode.InternalServerError
                };
            }

        }

        public async Task<IBaseResponse<bool>> DeleteFurniture(int id)
        {
            try
            {
                var furniture = await _furnitureRepository.GetById(id);

                if (furniture.Name == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = $"Furniture not found",
                        CodeStatus = StatusCode.FurnitureNotFound
                    };
                }

                if (await _furnitureRepository.Delete(furniture))
                {
                    return new BaseResponse<bool>()
                    {
                        CodeStatus = StatusCode.OK
                    };
                }
                else
                {
                    return new BaseResponse<bool>()
                    {
                        CodeStatus = StatusCode.DeleteNotFound
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = "[DeleteFurniture] ||: " + ex + " ||: " + ex.Message,
                    CodeStatus = StatusCode.InternalServerError
                };
            }
        }

    }
}
