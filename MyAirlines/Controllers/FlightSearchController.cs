using AutoMapper;
using FlightProject.Domain.DataDB;
using FlightProject.Domain.EntitiesDB;
using FlightProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyAirlines.ViewModels;
using Newtonsoft.Json;

namespace MyAirlines.Controllers
{
    public class FlightSearchController : Controller
    {
        //private readonly IService<Place> _service;
        private readonly IService<ArrivalPlace> _arrivalService;
        private readonly IService<DeparturePlace> _departureService;

        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        


        public FlightSearchController(/*IService<Place> service*/ IService<ArrivalPlace> arrivalService, IService<DeparturePlace> departureService, IFlightService flightService, IMapper mapper)
        {
            _flightService = flightService;
            _mapper = mapper;
            //_service = service;
            _arrivalService = arrivalService;
            _departureService = departureService;
            

        }

        //Get flights by place (=airport)
        [HttpGet]
        public async Task<IActionResult> Search()
        {
            try
            {
                ViewData["Title"] = "Search";

                FlightSearchVM vm = new FlightSearchVM();

                // Haal de departure places op en zet ze om naar DeparturePlaceVM
                var departurePlaces = await _departureService.GetAllAsync();
                var departurePlaceVMs = _mapper.Map<List<DeparturePlaceVM>>(departurePlaces);
                vm.AirportDeparture = new SelectList(departurePlaceVMs, "Id", "Name");

                // Haal de arrival places op en zet ze om naar ArrivalPlaceVM
                var arrivalPlaces = await _arrivalService.GetAllAsync();
                var arrivalPlaceVMs = _mapper.Map<List<ArrivalPlaceVM>>(arrivalPlaces);
                vm.AirportArrival = new SelectList(arrivalPlaceVMs, "Id", "Name");

                return View(vm);
            }
            catch (Exception ex)
            {
                // Handle error
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Search(FlightSearchVM model)
        {
            // Controleer of de dropdowns leeg zijn, geef anders een foutmelding
            if (model.SelectedDepartureId == null || model.SelectedArrivalId == null)
            {
                // Voeg een foutmelding toe in ModelState
                ModelState.AddModelError("", "Please select both departure and arrival airports.");
                return View(model);  // Weer terug naar de View met model errors
            }

            try
            {
                

                var list = await _flightService.GetFlightsFromTwoAirports(
                    model.SelectedDepartureId.Value,
                    model.SelectedArrivalId.Value
                );

                if (model.DepartureDate.HasValue)
                {
                    var targetDate = DateOnly.FromDateTime(model.DepartureDate.Value); //haal alleen de datum eruit

                    list = list
                        .Where(f => f.DepartureTime.HasValue &&
                                    DateOnly.FromDateTime(f.DepartureTime.Value) == targetDate) //vergelijk met elkaar
                        .ToList();
                }


                FlightSearchVM flightSearchVM = new FlightSearchVM
                {
                    SelectedDepartureId = model.SelectedDepartureId,  // Kopieer de geselecteerde waarden
                    SelectedArrivalId = model.SelectedArrivalId,
                    Flights = _mapper.Map<List<FlightVM>>(list)
                };

                // Herinstellen van de SelectLists met de juiste geselecteerde waarden
                var departurePlaces = await _departureService.GetAllAsync();
                var departurePlaceVMs = _mapper.Map<List<DeparturePlaceVM>>(departurePlaces);
                flightSearchVM.AirportDeparture = new SelectList(departurePlaceVMs, "Id", "Name", model.SelectedDepartureId);

                var arrivalPlaces = await _arrivalService.GetAllAsync();
                var arrivalPlaceVMs = _mapper.Map<List<ArrivalPlaceVM>>(arrivalPlaces);
                flightSearchVM.AirportArrival = new SelectList(arrivalPlaceVMs, "Id", "Name", model.SelectedArrivalId);

                return View("Search", flightSearchVM);
            }
            catch (Exception ex)
            {
                // Log het eventuele probleem
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }

    }
}
