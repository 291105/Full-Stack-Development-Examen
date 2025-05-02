using FlightProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Services.Interfaces
{
    public interface IAirportService : IService<Airport>
    {
        Task<List<Airport>> GetAllDepartureAirports();
        Task<List<Airport>> GetAllArrivalAirports();
    }
}
