using Furnituremarket.DAL.Interfaces;
using Furnituremarket.Domain.Enum;
using Furnituremarket.Domain.Model;
using Furnituremarket.Domain.Response;
using Furnituremarket.Domain.Response.Interfaces;
using Furnituremarket.Service.Interfaces;
using System;
using System.Threading.Tasks;

namespace Furnituremarket.Service.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IBaseResponse<Order>> CreateOrder()
        {
            try
            {
                var order = await _orderRepository.Create();

                if (order == null)//проверить
                {
                    return new BaseResponse<Order>()
                    {
                        Description = $"Order uncreated",
                        CodeStatus = StatusCode.Uncreated
                    };
                }
                return new BaseResponse<Order>()
                {
                    Data = order,
                    CodeStatus = StatusCode.OK
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<Order>()
                {
                    Description = "[CreateOrder] ||: " + ex + " ||: " + ex.Message,
                    CodeStatus = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Order>> GetByIdOrder(int idOrder)
        {
            try
            {
                var order = await _orderRepository.GetById(idOrder);

                if (order == null)//проверить
                {
                    return new BaseResponse<Order>()
                    {
                        Description = $"Order not found",
                        CodeStatus = StatusCode.NotFound
                    };
                }
                return new BaseResponse<Order>()
                {
                    Data = order,
                    CodeStatus = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Order>()
                {
                    Description = "[GetByIdOrder] ||: " + ex + " ||: " + ex.Message,
                    CodeStatus = StatusCode.InternalServerError
                };
            }
        }

        //TODO изменить реализацию обновления
        public async Task<IBaseResponse<Order>> UpdateOrder(int idOrder)
        {
            try
            {
                var order = await _orderRepository.GetById(idOrder);

                if (order == null)//проверить
                {
                    return new BaseResponse<Order>()
                    {
                        Description = $"Order not found",
                        CodeStatus = StatusCode.NotFound
                    };
                }

                order = await _orderRepository.Update(order);

                return new BaseResponse<Order>()
                {
                    Data = order,
                    CodeStatus = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Order>()
                {
                    Description = "[UpdateOrder] ||: " + ex + " ||: " + ex.Message,
                    CodeStatus = StatusCode.InternalServerError
                };
            }
        }
    }
}
