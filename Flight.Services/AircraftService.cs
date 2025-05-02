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
    public class AircraftService : IAircraftService
    {
        private readonly IAircraftDAO _aircraftDAO;

        public AircraftService(IAircraftDAO aircraftDAO)
        {
            _aircraftDAO = aircraftDAO;
        }


        public async Task<double> getPriceByClass(int aircraftId, Class cl)
        {
            return await _aircraftDAO.getPriceByClass(aircraftId, cl);
        }
    }
}
