using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Services.Interfaces
{
    public interface IFlightTicketService 
    {
        Task makeFlightTicket(int flightId, int ticketId);
    }
}
