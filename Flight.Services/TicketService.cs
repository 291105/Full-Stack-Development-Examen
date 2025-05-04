using FlightProject.Repositories.Interfaces;
using FlightProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketDAO _ticketDAO;
        public TicketService(ITicketDAO ticketDAO) {
            _ticketDAO = ticketDAO;
        }

        public async Task<int> MakeTicket(string FirstName, string LastName, string NationalRegisterNumber, double Price, int MealId, int ClassId, int BookingId, string Departure, string Arrival, DateTime DepartureTime, DateTime ArrivalTime)
        {
            return await _ticketDAO.MakeTicket(FirstName, LastName, NationalRegisterNumber, Price, MealId, ClassId, BookingId, Departure, Arrival, DepartureTime, ArrivalTime);
        }
    }
}
