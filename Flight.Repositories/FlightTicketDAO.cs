using FlightProject.Domain.Data;
using FlightProject.Domain.Entities;
using FlightProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Repositories
{
    public class FlightTicketDAO : IFlightTicketDAO
    {
        private readonly FullStackDbContext _db;
        public FlightTicketDAO(FullStackDbContext db)
        {
            _db = db;
        }
        public async Task makeFlightTicket(int flightId, int ticketId)
        {
            var flightTicket = new FlightTicket();
            flightTicket.FlightId = flightId;
            flightTicket.TicketId = ticketId;

            await _db.AddAsync(flightTicket);
            await _db.SaveChangesAsync();
        }
    }
}
