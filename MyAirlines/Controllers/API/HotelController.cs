using FlightProject.Domain.Data;
using FlightProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MyAirlines.ViewModels;

namespace MyAirlines.Api.Controllers
{
    public class HotelController : Controller
    {
        private readonly IHotelsApiService _hotelsService;

        public HotelController(IHotelsApiService hotelsService)
        {
            _hotelsService = hotelsService;
        }

        public async Task<IActionResult> Index(string city = "")
        {
            List<Hotel> results = new();
            if (!string.IsNullOrWhiteSpace(city))
            {
                results = await _hotelsService.GetHotelsAsync(city);
            }
            ViewData["City"] = city;
            return View(results);
        }
    }
}
