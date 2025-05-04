using FlightProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MyAirlines.Extentions;
using MyAirlines.ViewModels;
using System.Security.Claims;

namespace MyAirlines.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ITicketService _ticketService;
        private readonly IFlightTicketService _flightTicketService;

        public BookController(IBookingService bookingService, ITicketService ticketService, IFlightTicketService flightTicketService)
        {
            _bookingService = bookingService;
            _ticketService = ticketService;
            _flightTicketService = flightTicketService;
        }

        private async Task makeBookingAndTicketAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var booking = HttpContext.Session.GetObject<BookingVM>("CompleteBooking");

            //booking maken
            var bookingId = await _bookingService.Book(booking.TotalPricePerBooking, userId);

            //ticket maken en linken met de booking
            //voor elke vlucht die je neemt moet je een ticket maken en voor elke persoon ook nog eens
            foreach(var flight in booking.Flight)
            {
                foreach (var passenger in booking.Passengers)
                {
                   var ticketId = await _ticketService.MakeTicket(passenger.FirstName, passenger.LastName, passenger.NationalRegisterNumber, passenger.TicketPrice, (int) passenger.SelectedMealID, passenger.SelectedClassID, bookingId, flight.DeparturePlace, flight.ArrivalPlace, flight.DepartureTime, flight.ArrivalTime);
                    //FlightTicket tabel invullen
                    await _flightTicketService.makeFlightTicket(flight.FlightId, ticketId);
                }
            }
        }
        
        public async Task<IActionResult> BookAsync()
        {
            await makeBookingAndTicketAsync();
            
            return View("Index");
        }
    }
}
