using Furnituremarket.Domain.Model;
using Furnituremarket.Domain.Response.Interfaces;
using Furnituremarket.Domain.ViewModels.Cart;
using Furnituremarket.Service.Implementations;
using Furnituremarket.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Furnituremarket.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IFurnitureService _furnitureService;
        private ILogger<AccountService> _logger;
        public CartController(IOrderService cartService,
                              IFurnitureService furnitureService,
                              ILogger<AccountService> logger)
        {
            _orderService = cartService;
            _furnitureService = furnitureService;
            _logger = logger;
        }

        
        public async Task<IActionResult> AddCartFurniture(int id)
        {
            CartViewModel cartViewModel;
            Order order;
            IBaseResponse<Order> response;

            if (!HttpContext.Session.TryGetCart(out cartViewModel))
            {
                response = await _orderService.CreateOrder();
                if (response.CodeStatus == Domain.Enum.StatusCode.OK)
                    cartViewModel = new CartViewModel(((Order)response.Data).Id);
                else
                {
                    _logger.LogError(response.Description);
                    return RedirectToAction("Error");
                }
            }
            else
            {
                response = await _orderService.GetByIdOrder(cartViewModel.OrderId);
                if (response.CodeStatus != Domain.Enum.StatusCode.OK)
                {
                    _logger.LogError(response.Description);
                    return RedirectToAction("Error");
                }
            }

            order = (Order)response.Data;

            var furnitureResponse = await _furnitureService.GetFurnitureById(id);
            if (response.CodeStatus != Domain.Enum.StatusCode.OK)
            {
                _logger.LogError(response.Description);
                return RedirectToAction("Error");
            }


            order.AddItem((Furniture)furnitureResponse.Data, 1);

            //orderRepository.Update(order);

            cartViewModel.TotalCount = order.TotalCount;
            cartViewModel.TotalPrice = order.TotalPrice;

            HttpContext.Session.Set(cartViewModel);

            return RedirectToAction("GetAllFurniture", "Furniture", new { id });
        }
    }
}
