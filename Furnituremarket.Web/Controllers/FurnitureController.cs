using Furnituremarket.Domain.Model;
using Furnituremarket.Domain.Response.Interfaces;
using Furnituremarket.Service.Interfaces;
using Furnituremarket.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
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

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteFurniture(int id)
        {
            var response = await _furnitureService.DeleteFurniture(id);
            if (response.CodeStatus == Domain.Enum.StatusCode.OK)
                return RedirectToAction("GetAllFurniture");
            return RedirectToAction("Error");
        }



        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Save(Furniture model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    //byte[] imageData;
                    //using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                    //{
                    //    imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                    //}
                    //await _carService.Create(model, imageData);
                    await _furnitureService.CreateFurniture(model);
                }
                else
                {
                    await _furnitureService.UpdateFurniture(model.Id, model);
                }
                return RedirectToAction("GetAllFurniture");
            }
            return View();


        }

        //[Authorize(Roles = "Admin")]
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
