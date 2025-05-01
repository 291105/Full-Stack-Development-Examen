using Microsoft.AspNetCore.Mvc;
using FlightProject.Services;
using MyAirlines.ViewModels;
using FlightProject.Domain.Entities;

namespace RapidAPItest.Controllers
{
    public class HotelController : Controller
    {
        private readonly HotelsApiService _hotelsService;

        public HotelController(HotelsApiService hotelsService)
        {
            _hotelsService = hotelsService;
        }

        // GET /Hotel?city=Paris
        public async Task<IActionResult> Index(string city = "")
        {
            List<HotelSearchResult> results = new();
            if (!string.IsNullOrWhiteSpace(city))
            {
                results = await _hotelsService.SearchHotelsAsync(city);
            }
            ViewData["City"] = city;
            return View(results);
        }
    }
}
