using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using MyAirlines.Models;
using System.Diagnostics;
using System.Globalization;

namespace MyAirlines.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetAppLanguage(string lang, string returnUrl)
        {
            if (!string.IsNullOrEmpty(lang))
            {
                var culture = new CultureInfo(lang);

                // Deze cookie wordt door .NET gelezen
                var cookieValue = CookieRequestCultureProvider.MakeCookieValue(
                    new RequestCulture(culture));

                Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    cookieValue,
                    new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddYears(1),
                        IsEssential = true // zorg dat hij niet genegeerd wordt door cookiebeleid
                    });
            }

            return LocalRedirect(returnUrl ?? "/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

