using AutoMapper;
using FlightProject.Domain.Data;
using FlightProject.Domain.Entities;
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
        private readonly IService<Meal> _mealService;
        private readonly IService<Class> _classService;

        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;
        


        public FlightSearchController(/*IService<Place> service*/ IService<ArrivalPlace> arrivalService, IService<DeparturePlace> departureService, IFlightService flightService, IMapper mapper, IService<Meal> mealservice, IService<Class> classService)
        {
            _flightService = flightService;
            _mapper = mapper;
            //_service = service;
            _arrivalService = arrivalService;
            _departureService = departureService;
            _mealService = mealservice;
            _classService = classService;
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

                // Bereken de datumbereiken voor de zoekperiode (3 dagen na vandaag tot 6 maanden na vandaag)
                DateTime minDate = DateTime.Now.AddDays(3);  // 3 dagen na de huidige datum
                DateTime maxDate = DateTime.Now.AddMonths(6); // 6 maanden na de huidige datum

                // Zet de datumbereiken in de ViewModel
                vm.MinDepartureTime = minDate;
                vm.MaxDepartureTime = maxDate;


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
                // Bereken de toegestane datums voor de zoekperiode (3 dagen na vandaag tot 6 maanden na vandaag)
                DateTime minDate = DateTime.Now.AddDays(3);  // 3 dagen na de huidige datum
                DateTime maxDate = DateTime.Now.AddMonths(6); // 6 maanden na de huidige datum

                // Controleer of de geselecteerde datum binnen het toegestane bereik ligt
                if (model.DepartureDate.HasValue)
                {
                    DateTime selectedDate = model.DepartureDate.Value;

                    if (selectedDate < minDate || selectedDate > maxDate)
                    {
                        ModelState.AddModelError("DepartureDate", $"The departure date must be between {minDate.ToShortDateString()} and {maxDate.ToShortDateString()}.");
                        return View(model);  // Foutmelding als de datum buiten het bereik valt
                    }

                    var targetDate = DateOnly.FromDateTime(selectedDate); //haal alleen de datum eruit

                    // Haal de vluchten op
                    var list = await _flightService.GetFlightsFromTwoAirports(
                        model.SelectedDepartureId.Value,
                        model.SelectedArrivalId.Value
                    );

                    // Filter de vluchten op de gekozen datum
                    list = list
                        .Where(f => f.DepartureTime.HasValue &&
                                    DateOnly.FromDateTime(f.DepartureTime.Value) == targetDate) //vergelijk met elkaar
                        .ToList();

                    FlightSearchVM flightSearchVM = new FlightSearchVM
                    {
                        SelectedDepartureId = model.SelectedDepartureId,  // Kopieer de geselecteerde waarden
                        SelectedArrivalId = model.SelectedArrivalId,
                        Flights = _mapper.Map<List<FlightVM>>(list)  // Mappen naar FlightVM inclusief FlightId
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
                else
                {
                    ModelState.AddModelError("DepartureDate", "Please select a departure date.");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                // Log het eventuele probleem
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }




        /*
        [HttpGet]
        public async Task<IActionResult> BookFlight(int flightId)
        {
            if (flightId <= 0)
            {
                return NotFound();
            }

            // Haal de vlucht op
            var flight = await _flightService.getFlightById(flightId);

            if (flight == null)
            {
                return NotFound();
            }

            // Haal de beschikbare maaltijden op en zet ze om naar MealVM's
            var meals = await _mealService.GetAllAsync();
            var mealVMs = _mapper.Map<List<MealVM>>(meals);

            // Maak een SelectList voor de maaltijden
            var mealSelectList = new SelectList(mealVMs, "MealId", "Name");

            // Haal de beschikbare klassen op en zet ze om naar ClassVM's
            var classes = await _classService.GetAllAsync();
            var classVMs = _mapper.Map<List<ClassVM>>(classes);

            // Maak een SelectList voor de klassen
            var classSelectList = new SelectList(classVMs, "ClassId", "Name");

            // Zorg ervoor dat de lijst van Passengers altijd een geldige lijst is (zelfs als deze leeg is)
            var bookingViewModel = new BookingVM
            {
                FlightId = flightId,
                Passengers = new List<PassengerVM> { new PassengerVM() }, // Initialiseer altijd met minstens 1 passagier
                TotalPricePerBooking = 0 // Zorg ervoor dat de prijs geïnitialiseerd is
            };

            // Voeg de beschikbare maaltijden en klassen toe aan **alle** passagiers
            foreach (var passenger in bookingViewModel.Passengers)
            {
                // Wijs de SelectList toe voor de maaltijden en klassen (voor gebruik in de dropdowns)
                passenger.AvailableMeals = mealSelectList;
                passenger.AvailableClasses = classSelectList;
            }

            return View(bookingViewModel);
        }*/
    }
}
