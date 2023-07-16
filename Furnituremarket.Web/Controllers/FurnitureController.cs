using Furnituremarket.Domain.Model;
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
            if (query == null || string.IsNullOrWhiteSpace(query))
                return RedirectToAction("GetAllFurniture");
            var response = await _furnitureService.GetFurniture(query);
            if (response.CodeStatus == Domain.Enum.StatusCode.OK)
                return View("GetAllFurniture", response.Data);
            return RedirectToAction("Error");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteFurniture(int id)
        {
            var response = await _furnitureService.DeleteFurniture(id);
            if (response.CodeStatus == Domain.Enum.StatusCode.OK)
                return RedirectToAction("GetAllFurniture");
            return RedirectToAction("Error");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Save(Furniture model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0) await _furnitureService.CreateFurniture(model);
                else await _furnitureService.UpdateFurniture(model.Id, model);
                return RedirectToAction("GetAllFurniture");
            }
            return View();


        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
                return View(new Furniture());

            var response = await _furnitureService.GetFurnitureById(id);

            if (response.CodeStatus == Domain.Enum.StatusCode.OK)
                return View(response.Data);

            return RedirectToAction("Error");
        }

    }
}
