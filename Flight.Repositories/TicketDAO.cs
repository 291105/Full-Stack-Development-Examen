using FlightProject.Domain.Data;
using FlightProject.Domain.Entities;
using FlightProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Repositories
{
    public class TicketDAO : ITicketDAO
    {
        private readonly FullStackDbContext _db;
        public TicketDAO(FullStackDbContext db)
        {
            _db = db;
        }
        public async Task<int> MakeTicket(string FirstName, string LastName, string NationalRegisterNumber, double Price, int MealId, int ClassId, int BookingId, string Departure, string Arrival, DateTime DepartureTime, DateTime ArrivalTime)
        {
            // Zoek een beschikbare stoel in de juiste klasse
            var availableSeat = await _db.Seats
                .Where(s => s.ClassId == ClassId && s.IsOccupied == false)
                .FirstOrDefaultAsync();

            if (availableSeat == null)
            {
                throw new Exception("Geen beschikbare stoelen gevonden in de opgegeven klasse.");
            }

            // Markeer stoel als bezet
            availableSeat.IsOccupied = true;

            var ticket = new Ticket
            {
                FirstName = FirstName,
                LastName = LastName,
                NationalRegisterNumber = NationalRegisterNumber,
                Price = Price,
                SeasonId = 2,
                SeatId = availableSeat.SeatId,
                MealId = MealId,
                ClassId = ClassId,
                BookingId = BookingId,
                Departure = Departure,
                Arrival = Arrival,
                DepartureTime = DepartureTime,
                ArrivalTime = ArrivalTime
            };

            await _db.Tickets.AddAsync(ticket);
            await _db.SaveChangesAsync(); // Slaat ticket én stoelupdate op

            return ticket.TicketId;
        }

        public async Task<List<Ticket>> getTicketsByBookingId(int bookingId)
        {
            return await _db.Tickets.Where(b => b.BookingId == bookingId).ToListAsync();
        }
    }
}
