using FlightProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Services.Interfaces
{
    public interface ITicketService
    {
        Task<int> MakeTicket(string FirstName, string LastName, string NationalRegisterNumber, double Price, int MealId, int ClassId, int BookingId, string Departure, string Arrival, DateTime DepartureTime, DateTime ArrivalTime);
        Task<List<Ticket>> getTicketsByBookingId(int bookingId);
    }
}
