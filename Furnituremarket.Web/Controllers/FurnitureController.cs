using Furnituremarket.Domain.Response.Interfaces;
using Furnituremarket.Domain.ViewModels.Furniture;
using Furnituremarket.Service.Interfaces;
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

        [HttpGet]
        public async Task<IActionResult> GetFurnitureByName(string name)
        {
            var response = await _furnitureService.GetFurnitureByName(name);
            if (response.CodeStatus == Domain.Enum.StatusCode.OK)
                return View(response.Data);
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetFurnitureById(int id)
        {
            var response = await _furnitureService.GetFurnitureById(id);
            if (response.CodeStatus == Domain.Enum.StatusCode.OK)
                return View(response.Data);
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFurniture()
        {
            var response = await _furnitureService.GetAllFurniture();
            if (response.CodeStatus == Domain.Enum.StatusCode.OK)
                return View(response.Data.ToList());
            return RedirectToAction("Error");
        }
    }
}
