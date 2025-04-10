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
    public class ArrivalPlaceService : IService<ArrivalPlace>
    {
        private readonly IDAO<ArrivalPlace> _arrivalDAO;

        public ArrivalPlaceService(IDAO<ArrivalPlace> arrivalDAO)
        {
            _arrivalDAO = arrivalDAO;
        }

        public async Task<IEnumerable<ArrivalPlace>?> GetAllAsync()
        {
            return await _arrivalDAO.GetAllAsync();
        }
    }
}
