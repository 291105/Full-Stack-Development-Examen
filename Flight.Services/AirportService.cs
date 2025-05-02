using FlightProject.Domain.Entities;
using FlightProject.Repositories.Interfaces;
using FlightProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Services
{
    public class AirportService : IAirportService
    {
        private readonly IAirportDAO _airportDAO;

        public AirportService(IAirportDAO airportDAO)
        {
            _airportDAO = airportDAO;
        }

        public async Task<List<Airport>> GetAllArrivalAirports()
        {
            return await _airportDAO.GetAllArrivalAirports();
        }

        public async Task<IEnumerable<Airport>?> GetAllAsync()
        {
            return await _airportDAO.GetAllAsync();
        }

        public async Task<List<Airport>> GetAllDepartureAirports()
        {
            return await _airportDAO.GetAllDepartureAirports();
        }
    }
}
