using FlightProject.Domain.EntitiesDB;
using FlightProject.Repositories.Interfaces;
using FlightProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightDAO _flightDAO;

        public FlightService(IFlightDAO flightdao)
        {
            _flightDAO = flightdao;
        }
        public async Task<IEnumerable<Flight>?> GetAllAsync()
        {
            return await _flightDAO.GetAllAsync();
        }

        public async Task<List<Flight>> GetFlightsFromTwoAirports(int airportID1, int airportID2)
        {
            return await _flightDAO.GetFlightsFromTwoAirports(airportID1, airportID2);
        }

        
    }
}
