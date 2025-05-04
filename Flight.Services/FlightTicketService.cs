using FlightProject.Repositories.Interfaces;
using FlightProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Services
{
    public class FlightTicketService : IFlightTicketService
    {
        private readonly IFlightTicketDAO _flightTicketDAO;
        public FlightTicketService(IFlightTicketDAO flightTicketDAO) {
            _flightTicketDAO = flightTicketDAO;
        }

        public async Task makeFlightTicket(int flightId, int ticketId)
        {
            await _flightTicketDAO.makeFlightTicket(flightId, ticketId);
        }
    }
}
