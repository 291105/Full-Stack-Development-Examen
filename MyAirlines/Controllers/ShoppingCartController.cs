using Microsoft.AspNetCore.Mvc;
using MyAirlines.Extentions;
using MyAirlines.ViewModels;

namespace MyAirlines.Controllers
{
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            ShoppingCartVM? cartList = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            return View(cartList);
        }

        [HttpPost]
        public IActionResult DeleteCartItem(int index)
        {
            var cart = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");

            if (cart != null && index >= 0 && index < cart.Carts.Count)
            {
                cart.Carts.RemoveAt(index);
                HttpContext.Session.SetObject("ShoppingCart", cart);
            }

            return RedirectToAction("Index");
        }

    }
}
