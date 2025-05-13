using FlightProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Services.Interfaces
{
    public interface IFlightService : IService<Flight>
    {
        Task<List<List<Flight>>> GetFlightsFromTwoAirports(int airportID1, int airportID2);
        Task<List<List<Flight>>> GetAvailableFlights(int departureAirportId, int arrivalAirportId, int selectedClassId, int requiredSeats, DateOnly? targetDate);


        Task<Flight> getFlightById(int id);
    }
}
