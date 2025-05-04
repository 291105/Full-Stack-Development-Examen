using FlightProject.Domain.Data;
using FlightProject.Domain.Entities;
using FlightProject.Repositories.Interfaces;
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
            var ticket = new Ticket();
            ticket.FirstName = FirstName;
            ticket.LastName = LastName;
            ticket.NationalRegisterNumber = NationalRegisterNumber;
            ticket.Price = Price;
            ticket.SeasonId = 2;
            ticket.SeatNumber = "1";
            ticket.MealId = MealId;
            ticket.ClassId = ClassId;
            ticket.BookingId = BookingId;
            ticket.Departure = Departure;
            ticket.Arrival = Arrival;
            ticket.DepartureTime = DepartureTime;
            ticket.ArrivalTime = ArrivalTime;

            await _db.Tickets.AddAsync(ticket);
            await _db.SaveChangesAsync();
            return ticket.TicketId;
        }

        
    }
}
