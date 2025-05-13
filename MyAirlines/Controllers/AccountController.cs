using AutoMapper;
using FlightProject.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MyAirlines.ViewModels;
using System.Security.Claims;

namespace MyAirlines.Controllers
{
    public class AccountController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        public AccountController(IBookingService bookingService, IMapper mapper)
        {
            _bookingService = bookingService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var bookingForAccountVMList = new List<BookingsForAccountVM>();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var bookings = await _bookingService.GetAllBookingsByUser(userId);

            foreach (var booking in bookings)
            {
                var bookingForAccountVM = new BookingsForAccountVM
                {
                    BookingId = booking.BookingId,
                    Tickets = _mapper.Map<List<TicketVM>>(booking.Tickets)
                };

                bookingForAccountVMList.Add(bookingForAccountVM);
            }

            return View(bookingForAccountVMList);
        }

    }
}
