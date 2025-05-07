using FlightProject.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyAirlines.Extentions;
using MyAirlines.ViewModels;
using SendMail.Util.Mail.Interfaces;
using SendMail.Util.PDF.Interfaces;
using System.Security.Claims;

namespace MyAirlines.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ITicketService _ticketService;
        private readonly IFlightTicketService _flightTicketService;
        //om email te kunnen verzenden
        private readonly IEmailSend _emailSend;
        //om de pdf te kunnen maken
        private readonly ICreatePDF _createPDF;
        //om useremail te kunnen ophalen
        private readonly UserManager<IdentityUser> _userManager;

        public BookController(IBookingService bookingService, ITicketService ticketService, IFlightTicketService flightTicketService, UserManager<IdentityUser> userManager, IEmailSend emailSend, ICreatePDF createPDF)
        {
            _bookingService = bookingService;
            _ticketService = ticketService;
            _flightTicketService = flightTicketService;
            _userManager = userManager;
            _emailSend = emailSend;
            _createPDF = createPDF;
        }

        private async Task makeBookingAndTicketAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var booking = HttpContext.Session.GetObject<BookingVM>("CompleteBooking");
            //dit haal ik op omdat als ik apart twee tickets erin steek dan gaan ze niet beide geboekt worden
            var shoppingcart = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");
            
            //booking maken
            var bookingId = await _bookingService.Book(booking.TotalPricePerBooking, userId);

            foreach (var cart in shoppingcart.Carts)
            {
                foreach (var flight in cart.Flights)
                {
                    if (!booking.Flight.Any(f => f.FlightId == flight.FlightId))
                    {
                        booking.Flight.Add(flight);
                    }
                }
            }
            //ticket maken en linken met de booking
            //voor elke vlucht die je neemt moet je een ticket maken en voor elke persoon ook nog eens
            foreach (var flight in booking.Flight)
            {
                foreach (var passenger in booking.Passengers)
                {
                   var ticketId = await _ticketService.MakeTicket(passenger.FirstName, passenger.LastName, passenger.NationalRegisterNumber, passenger.TicketPrice, (int) passenger.SelectedMealID, passenger.SelectedClassID, bookingId, flight.DeparturePlace, flight.ArrivalPlace, flight.DepartureTime, flight.ArrivalTime);
                    //FlightTicket tabel invullen
                    await _flightTicketService.makeFlightTicket(flight.FlightId, ticketId);
                }
            }

            //Zorgen dat er mail verzonden wordt met de tickets
            //krijg lijst van tickets per booking
            var tickets = await _ticketService.getTicketsByBookingId(bookingId);

            //de pdf maken
            var pdf = _createPDF.CreatePDFDocumentAsync(tickets, "avaro_white.svg");


            //de mail van de user is nodig
            var user = await _userManager.FindByIdAsync(userId);
            var userEmail = await _userManager.GetEmailAsync(user);


            //email verzenden met de ticket
            await _emailSend.SendEmailAttachmentAsync(userEmail, "Tickets", "Dear Customer, In the attachment you will find your tickets. We hope you enjoy your flight", pdf, "Tickets", true);
        }

        public async Task<IActionResult> BookAsync()
        {
            await makeBookingAndTicketAsync();
            
            return View("Index");
        }
    }
}
