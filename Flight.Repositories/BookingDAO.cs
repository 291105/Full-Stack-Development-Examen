using FlightProject.Domain.Data;
using FlightProject.Domain.Entities;
using FlightProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Repositories
{
    public class BookingDAO : IBookingDAO
    {
        private readonly FullStackDbContext _db;
        public BookingDAO(FullStackDbContext db)
        {
            _db = db;
        }
       

        public async Task<int> Book(double TotalPrice, string UserId)
        {
            var booking = new Booking();
            booking.TotalPricePerBooking = TotalPrice;
            booking.UserId = UserId;

            await _db.Bookings.AddAsync(booking);
            await _db.SaveChangesAsync();

            return booking.BookingId;
        }

        public async Task<Booking> GetBookingById(int id)
        {
            return await _db.Bookings.Where(b => b.BookingId == id).FirstOrDefaultAsync();
        }
    }
}
