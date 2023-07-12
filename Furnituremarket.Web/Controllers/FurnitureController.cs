using Furnituremarket.Domain.Response.Interfaces;
using Furnituremarket.Domain.ViewModels.Furniture;
using Furnituremarket.Service.Interfaces;
using Furnituremarket.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Furnituremarket.Web.Controllers
{
    public class FurnitureController : Controller
    {
        private readonly IFurnitureService _furnitureService;

        public FurnitureController(IFurnitureService furnitureService)
        {
            _furnitureService = furnitureService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFurniture()
        {
            var response = await _furnitureService.GetAllFurniture();
            if (response.CodeStatus == Domain.Enum.StatusCode.OK)
                return View("GetAllFurniture", response.Data.ToList());
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetFurnitureById(int id)
        {
            var response = await _furnitureService.GetFurnitureById(id);
            if (response.CodeStatus == Domain.Enum.StatusCode.OK)
                return View("GetFurnitureById", response.Data);
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetFurniture(string query)
        {
            var response = await _furnitureService.GetFurniture(query);
            if (response.CodeStatus == Domain.Enum.StatusCode.OK)
                return View("GetAllFurniture", response.Data);
            return RedirectToAction("Error");
        }

        [HttpPost]
        public IActionResult AddInCartFurniture(int id, decimal price)
        {
                Cart cart;

                if (!HttpContext.Session.TryGetCart(out cart))
                    cart = new Cart();

                if (cart.Items.ContainsKey(id)) cart.Items[id]++;
                else cart.Items[id] = 1;

                cart.Amount += price;

                HttpContext.Session.Set(cart);

                return RedirectToAction("GetFurnitureById", "Furniture", new {id});   
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(FurnitureViewModel model)
        {
            IBaseResponse<bool> response = null;

            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                    response = await _furnitureService.CreateFurniture(model);
                else
                    response = await _furnitureService.UpdateFurniture(model.Id, model);
            }
            if (response.CodeStatus == Domain.Enum.StatusCode.OK)
                return View("GetCars");
            return RedirectToAction("Error");
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
                return View();

            var response = await _furnitureService.GetFurnitureById(id);

            if (response.CodeStatus == Domain.Enum.StatusCode.OK)
                return View(response.Data);
            return RedirectToAction("Error");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFurniture(int id)
        {
            var response = await _furnitureService.DeleteFurniture(id);
            if (response.CodeStatus == Domain.Enum.StatusCode.OK)
                return View(response.Data);
            return RedirectToAction("Error");
        }
    
    }
}
