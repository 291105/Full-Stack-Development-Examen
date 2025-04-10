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
    public class AirportService : IService<Place>
    {
        private readonly IDAO<Place> _airportDAO;

        public AirportService(IDAO<Place> airportDAO)
        {
            _airportDAO = airportDAO;
        }

        public async Task<IEnumerable<Place>?> GetAllAsync()
        {
           return await _airportDAO.GetAllAsync();
        }
    }
}
