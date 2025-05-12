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
        //om mealId te kunnen vinden
        private readonly IMealService _mealService;
        //om classId te kunnen vinden
        private readonly IClassService _classService;

        public BookController(IMealService mealservice, IClassService classservice, IBookingService bookingService, ITicketService ticketService, IFlightTicketService flightTicketService, UserManager<IdentityUser> userManager, IEmailSend emailSend, ICreatePDF createPDF)
        {
            _bookingService = bookingService;
            _ticketService = ticketService;
            _flightTicketService = flightTicketService;
            _userManager = userManager;
            _emailSend = emailSend;
            _createPDF = createPDF;
            _mealService = mealservice;
            _classService = classservice;
        }

        private async Task makeBookingAndTicketAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var booking = HttpContext.Session.GetObject<BookingVM>("CompleteBooking");
            //dit haal ik op omdat als ik apart twee tickets erin steek dan gaan ze niet beide geboekt worden
            var shoppingcart = HttpContext.Session.GetObject<ShoppingCartVM>("ShoppingCart");


            //zorgen dat de prijs van booking aangepast wordt naar de prijs van in de winkelwagen want je kan daar nog vluchten staan hebben!
            booking.TotalPricePerBooking = 0; //terug op nul zetten en opnieuw berekenen
            foreach (var cart in shoppingcart.Carts)
            {
                booking.TotalPricePerBooking += cart.Price;
            }

            //booking maken
            var bookingId = await _bookingService.Book(booking.TotalPricePerBooking, userId);
            booking.Flight = [];



            //ticket maken en linken met de booking
            //voor elke vlucht die je neemt moet je een ticket maken en voor elke persoon ook nog eens
            foreach (var cart in shoppingcart.Carts)
            {
                foreach (var flight in cart.Flights)
                {
                    var ticketId = await _ticketService.MakeTicket(cart.FirstName, cart.LastName, cart.NationalRegisterNumber, cart.Price, await _mealService.GetMealIdByMealName(cart.MealName), await _classService.getClassIdByClassName(cart.ClassName), bookingId, flight.DeparturePlace, flight.ArrivalPlace, flight.DepartureTime, flight.ArrivalTime);
                    //FlightTicket tabel invullen
                    await _flightTicketService.makeFlightTicket(flight.FlightId, ticketId);
                }
            }

            

            //Zorgen dat er mail verzonden wordt met de tickets
            //krijg lijst van tickets per booking
            var tickets = await _ticketService.getTicketsByBookingId(bookingId);
            
            //de pdf maken get booking by id
            var pdf = await _createPDF.CreatePDFDocumentAsync(tickets, await _bookingService.GetBookingById(bookingId) ,"C:\\Users\\Gebruiker\\source\\repos\\MyAirlines\\Full-Stack-Development-Examen\\MyAirlines\\wwwroot\\Images\\avaro.png");


            //de mail van de user is nodig
            var user = await _userManager.FindByIdAsync(userId);
            var userEmail = await _userManager.GetEmailAsync(user);


            //email verzenden met de ticket
            await _emailSend.SendEmailAttachmentAsync(userEmail, "Tickets", "Dear Customer, In the attachment you will find your tickets. We hope you enjoy your flight.", pdf, "Tickets", true);

            //winkelmandje wegdoen
            HttpContext.Session.Remove("ShoppingCart");
        }

        public async Task<IActionResult> BookAsync()
        {
            await makeBookingAndTicketAsync();
            
            return View("Index");
        }
    }
}
