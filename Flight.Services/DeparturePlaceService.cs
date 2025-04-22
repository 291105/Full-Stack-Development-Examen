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
    public class DeparturePlaceService : IService<DeparturePlace>   
    {
        private readonly IDAO<DeparturePlace> _departureDAO;

        public DeparturePlaceService(IDAO<DeparturePlace> departureDAO)
        {
            _departureDAO = departureDAO;
        }

        

        public async Task<IEnumerable<DeparturePlace>?> GetAllAsync()
        {
            return await _departureDAO.GetAllAsync();
        }
    }
}
