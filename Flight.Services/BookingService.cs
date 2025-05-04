using FlightProject.Repositories.Interfaces;
using FlightProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingDAO _bookingDAO;

        public BookingService(IBookingDAO bookingDAO)
        {
            _bookingDAO = bookingDAO;
        }
        public async Task<int> Book(double TotalPrice, string UserId)
        {
            return await _bookingDAO.Book(TotalPrice, UserId);
        }
    }
}
