using Furnituremarket.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Furnituremarket.Web.Controllers
{
    public class CartController : Controller
    {
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

            return RedirectToAction("GetAllFurniture", "Furniture", new { id });
        }
    }
}
