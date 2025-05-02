using FlightProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Repositories.Interfaces
{
    public interface IAirportDAO : IDAO<Airport>
    {
        Task<List<Airport>> GetAllDepartureAirports();
        Task<List<Airport>> GetAllArrivalAirports();
        
    }
}
