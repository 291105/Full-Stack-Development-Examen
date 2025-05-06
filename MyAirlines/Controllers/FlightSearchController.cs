using AutoMapper;
using FlightProject.Domain.Data;
using FlightProject.Domain.Entities;
using FlightProject.Repositories.Interfaces;
using FlightProject.Services;
using FlightProject.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyAirlines.Extentions;
using MyAirlines.ViewModels;
using Newtonsoft.Json;

namespace MyAirlines.Controllers
{
    public class FlightSearchController : Controller
    {
        //private readonly IService<Place> _service;
        private readonly IAirportService _airportService;

        private readonly IMealService _mealService;
        private readonly IClassService _classService;
        private readonly IAircraftService _aircraftService;

        private readonly IFlightService _flightService;
        private readonly IMapper _mapper;



        public FlightSearchController(IAircraftService aircraftservice, IAirportService airportService, IFlightService flightService, IMapper mapper, IMealService mealservice, IClassService classService)
        {
            _flightService = flightService;
            _mapper = mapper;
            //_service = service;
            _airportService = airportService;
            _mealService = mealservice;
            _classService = classService;
            _aircraftService = aircraftservice;
        }

        // GET: Search
        [HttpGet]
        public async Task<IActionResult> Search()
        {
            try
            {
                FlightSearchVM vm = new FlightSearchVM();

                // Haal de vertrek luchthavens op
                var departurePlaces = await _airportService.GetAllDepartureAirports();
                var departurePlaceVMs = _mapper.Map<List<AirportVM>>(departurePlaces);
                vm.AirportDeparture = new SelectList(departurePlaceVMs, "Id", "City");

                // Haal de aankomst luchthavens op
                var arrivalPlaces = await _airportService.GetAllArrivalAirports();
                var arrivalPlaceVMs = _mapper.Map<List<AirportVM>>(arrivalPlaces);
                vm.AirportArrival = new SelectList(arrivalPlaceVMs, "Id", "City");

                // blablabla

                // Haal de klassen op
                var classes = await _classService.GetAllAsync();
                var classVMs = _mapper.Map<List<ClassVM>>(classes);
                vm.Class = new SelectList(classVMs, "ClassId", "Name");

                // Stel datumbereik in (3 dagen na vandaag tot 6 maanden)
                DateTime minDate = DateTime.Now.AddDays(3);
                DateTime maxDate = DateTime.Now.AddMonths(6);
                vm.MinDepartureTime = minDate;
                vm.MaxDepartureTime = maxDate;

                return View(vm);
            }
            catch (Exception ex)
            {
                // Foutafhandelingslogica
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Search(FlightSearchVM model)
        {
            try
            {
                // Haal de vluchten op
                var availableFlights = await _flightService.GetAvailableFlights(
                    model.SelectedDepartureId.Value,
                    model.SelectedArrivalId.Value,
                    model.SelectedClassId ?? 0,
                    model.AantalPersonenNodig,
                    model.DepartureDate.Value
                );

                // Maak een lijst van FlightVM's
                var flightGroups = new List<FlightGroupVM>();
                foreach (var flightGroup in availableFlights)
                {
                    var flightVMs = new List<FlightVM>();  // Maak een nieuwe lijst voor FlightVM's

                    foreach (var flight in flightGroup)  // Itereer door elke Flight in de vluchtgroep
                    {
                        // Map elke Flight naar FlightVM
                        var flightVM = _mapper.Map<FlightVM>(flight);
                        flightVMs.Add(flightVM);  // Voeg de FlightVM toe aan de lijst
                    }

                    // Voeg de lijst van FlightVM's toe aan de flightGroup
                    flightGroups.Add(new FlightGroupVM
                    {
                        Flights = flightVMs,
                        TotalTravelTime = TimeSpan.FromMinutes(flightVMs.Where(f => f.Duration.HasValue)
                        .Sum(f => f.Duration.Value.TotalMinutes))
                    });
                }


                // Stel de zoekresultaten in voor de ViewModel
                var searchVM = new FlightSearchVM
                {
                    SelectedDepartureId = model.SelectedDepartureId,
                    SelectedArrivalId = model.SelectedArrivalId,
                    SelectedClassId = model.SelectedClassId,
                    AantalPersonenNodig = model.AantalPersonenNodig,
                    DepartureDate = model.DepartureDate,
                    MinDepartureTime = DateTime.Now.AddDays(3),
                    MaxDepartureTime = DateTime.Now.AddMonths(6),

                    FlightGroups = flightGroups
                };

                // Vul de dropdownlijsten opnieuw in
                var departurePlaces = await _airportService.GetAllDepartureAirports();
                var departurePlaceVMs = _mapper.Map<List<AirportVM>>(departurePlaces);
                searchVM.AirportDeparture = new SelectList(departurePlaceVMs, "Id", "City", searchVM.SelectedDepartureId);

                var arrivalPlaces = await _airportService.GetAllArrivalAirports();
                var arrivalPlaceVMs = _mapper.Map<List<AirportVM>>(arrivalPlaces);
                searchVM.AirportArrival = new SelectList(arrivalPlaceVMs, "Id", "City", searchVM.SelectedArrivalId);

                var classes = await _classService.GetAllAsync();
                var classVM = _mapper.Map<List<ClassVM>>(classes);
                searchVM.Class = new SelectList(classVM, "ClassId", "Name", searchVM.SelectedClassId);

                return View("Search", searchVM);
            }
            catch (Exception ex)
            {
                // Log het probleem en toon een foutpagina
                Console.WriteLine(ex.Message);
                return View("Error");
            }
        }


        //dit steekt alles in een session object zodat ik dit kan gebruiken in mijn volgende pagina!
        [HttpPost]
        public IActionResult PrepareBooking(string flightGroupVM, int aantalpersonen, int selectedClassId)
        {
            var deserializedFlightGroupVM = JsonConvert.DeserializeObject<FlightGroupVM>(flightGroupVM);

            HttpContext.Session.SetObject("FlightGroupVM", deserializedFlightGroupVM);
            HttpContext.Session.SetInt32("AantalPersonen", aantalpersonen);
            HttpContext.Session.SetInt32("SelectedClassId", selectedClassId);

            return RedirectToAction("BookFlight");
        }



        [HttpGet]
        public async Task<IActionResult> BookFlight()
        {

            var flightGroupVM = HttpContext.Session.GetObject<FlightGroupVM>("FlightGroupVM");
            var aantalPersonen = HttpContext.Session.GetInt32("AantalPersonen");
            var selectedClassId = HttpContext.Session.GetInt32("SelectedClassId");



            if (flightGroupVM == null)
            {
                // Als session leeg is, foutafhandeling
                return RedirectToAction("ErrorPage");
            }

            var bookingVm = new BookingVM
            {
                Flight = flightGroupVM.Flights,
                Passengers = new List<PassengerVM>()
            };
            //bereken de basisprijs door de prijs te nemen van de gekozen class
            //getPriceByClass and plane
            double price = 0;
            //for loop door de vluchten en per vlucht de prijs berekenen van de class in het vliegtuig en dit optellen
            foreach (var flight in flightGroupVM.Flights)
            {

                var aircraftId = flight.AircraftId;


                var selectedClass = await _classService.getClassById((int)selectedClassId);


                price += await _aircraftService.getPriceByClass(aircraftId, selectedClass);
            }


            //vind de name van de gekozen class
            var classes = await _classService.getClassById((int)selectedClassId);
            var classVms = _mapper.Map<ClassVM>(classes);



            for (int i = 0; i < aantalPersonen; i++)
            {
                var meals = await _mealService.GetAllMeals(flightGroupVM.Flights.Last().ArrivalPlace);

                var passengerVM = new PassengerVM
                {
                    Class = classVms,
                    Meals = _mapper.Map<List<MealVM>>(meals),
                    TicketPrice = price
                };
                bookingVm.TotalPricePerBooking += price;
                bookingVm.Passengers.Add(passengerVM);
            }

            HttpContext.Session.SetObject("BookingVM", bookingVm);

            return View(bookingVm);

        }

        //als alles is ingevuld dan duw je hierop en de ticket wordt ingevuld en in winkelmandje gezet
        [HttpPost]
        public async Task<IActionResult> BookFlight(List<PassengerVM> passengers, double totalPricePerBooking)
        {
            foreach (var passenger in passengers)
            {
                if (passenger.SelectedMealID == null)
                {
                    // Voeg een foutmelding toe aan TempData om deze door te geven aan de weergave
                    TempData["ErrorMessage"] = "Selecteer een maaltijd voor alle passagiers.";

                    // Stuur de gebruiker terug naar de vorige pagina
                    return RedirectToAction("BookFlight", "FlightSearch");
                }
            }

            var bookingVM = HttpContext.Session.GetObject<BookingVM>("BookingVM");

            var booking = new BookingVM();

            booking.Flight = bookingVM.Flight;
            booking.TotalPricePerBooking = (totalPricePerBooking / 100);

            var selectedClassId = HttpContext.Session.GetInt32("SelectedClassId");

            foreach (var passenger in passengers)
            {
                passenger.TicketPrice = passenger.TicketPrice / 100;
                passenger.SelectedClassID = (int) selectedClassId;
            }
            booking.Passengers = passengers;

            // Haal eventueel bestaande winkelwagen op
            var existingCart = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart") ?? new ShoppingCartVM
            {
                Carts = new List<CartVM>()
            };

            // Voor elke passagier maak een CartVM aan
            foreach (var passenger in booking.Passengers)
            {
                var meal = await _mealService.GetMealById((int)passenger.SelectedMealID);
                var newCartItem = new CartVM
                {
                    TicketId = 1,
                    Departure = booking.Flight.First().DeparturePlace,
                    Arrival = booking.Flight.Last().ArrivalPlace,
                    FirstName = passenger.FirstName,
                    LastName = passenger.LastName,
                    NationalRegisterNumber = passenger.NationalRegisterNumber,
                    ClassName = passenger.Class.Name,
                    MealName = meal.Name,
                    Price = passenger.TicketPrice,
                    DateCreated = DateTime.Now,
                    Flights = booking.Flight
                };

                existingCart.Carts.Add(newCartItem);
            }
            //zet mijn bookingvm ook in een session dat ik die bij booking kan gebruiken
            HttpContext.Session.SetObject("CompleteBooking", booking);

            // Zet terug in session
            HttpContext.Session.SetObject("ShoppingCart", existingCart);

            // Ga naar winkelwagen
            return RedirectToAction("Index", "ShoppingCart");
        }
    }


}