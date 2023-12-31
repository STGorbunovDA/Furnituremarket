﻿using Furnituremarket.Domain.Constants;
using Furnituremarket.Domain.Model;
using Furnituremarket.Domain.Response.Interfaces;
using Furnituremarket.Domain.ViewModels.Order;
using Furnituremarket.Service.Implementations;
using Furnituremarket.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Furnituremarket.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IFurnitureService _furnitureService;
        private ILogger<AccountService> _logger;
        public OrderController(IOrderService cartService,
                              IFurnitureService furnitureService,
                              ILogger<AccountService> logger)
        {
            _orderService = cartService;
            _furnitureService = furnitureService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> DetailOrder()
        {
            IBaseResponse<Order> response = null;

            if (HttpContext.Session.TryGetCart(out OrderViewModel orderViewModel))
            {
                response = await _orderService.GetByIdOrder(orderViewModel.OrderId);

                if (response.CodeStatus != Domain.Enum.StatusCode.OK)
                {
                    _logger.LogError(response.Description);
                    return RedirectToAction("Error");
                }
            }

            return View("Detail", response.Data);
        }


        [HttpPost]
        public async Task<IActionResult> ChangeOrder(int id, int flag)
        {
            OrderViewModel orderViewModel;
            Order order;
            IBaseResponse<Order> response;


            if (!HttpContext.Session.TryGetCart(out orderViewModel))
            {
                response = await _orderService.CreateOrder();
                if (response.CodeStatus == Domain.Enum.StatusCode.OK)
                    orderViewModel = new OrderViewModel(((Order)response.Data).Id);
                else
                {
                    _logger.LogError(response.Description);
                    return RedirectToAction("Error");
                }
            }
            else
            {
                response = await _orderService.GetByIdOrder(orderViewModel.OrderId);
                if (response.CodeStatus != Domain.Enum.StatusCode.OK)
                {
                    _logger.LogError(response.Description);
                    return RedirectToAction("Error");
                }
            }

            order = (Order)response.Data;

            var furnitureResponse = await _furnitureService.GetFurnitureById(id);

            if (furnitureResponse.CodeStatus != Domain.Enum.StatusCode.OK)
            {
                _logger.LogError(furnitureResponse.Description);
                return RedirectToAction("Error");
            }


            if (flag == RequiredСonstants.ADD_ORDERS_CONST)
                order.AddOrUpdateFurniture((Furniture)furnitureResponse.Data, 
                    RequiredСonstants.ADD_ORDERS_CONST);

            else if (flag == RequiredСonstants.DELETE_ORDERS_CONST)
            {
                foreach (var item in order.Items)
                {
                    if (item.FurnitureId == id)
                    {
                        order.RemoveFurniture((Furniture)furnitureResponse.Data);
                        break;
                    }
                }
            }
            else if (flag == RequiredСonstants.DELETE_ITEM_ORDERS_CONST)
            {
                foreach (var item in order.Items)
                {
                    if (item.FurnitureId == id)
                    {
                        order.RemoveItemFurniture(
                            (Furniture)furnitureResponse.Data, item.Count);
                        break;
                    }
                }
            }
            else if (flag == RequiredСonstants.ADD_ITEM_ORDERS_CONST)
            {
                foreach (var item in order.Items)
                {
                    if (item.FurnitureId == id)
                    {
                        order.AddItemFurniture(
                            (Furniture)furnitureResponse.Data, item.Count);
                        break;
                    }
                }
            }
            //orderRepository.Update(order);

            orderViewModel.TotalCount = order.TotalCount;
            orderViewModel.TotalPrice = order.TotalPrice;

            HttpContext.Session.Set(orderViewModel);
          

            if (flag == RequiredСonstants.DELETE_ORDERS_CONST && order.Items.Count > 0)
                return RedirectToAction("DetailOrder", "Order", new { id });

            else if (flag == RequiredСonstants.DELETE_ITEM_ORDERS_CONST && order.Items.Count > 0)
                return RedirectToAction("DetailOrder", "Order", new { id });
            
            else if (flag == RequiredСonstants.ADD_ITEM_ORDERS_CONST && order.Items.Count > 0)
                return RedirectToAction("DetailOrder", "Order", new { id });

            return RedirectToAction("GetAllFurniture", "Furniture", new { id });
        }
    }
}
