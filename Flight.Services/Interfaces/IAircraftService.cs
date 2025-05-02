using FlightProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Services.Interfaces
{
    public interface IAircraftService
    {
        Task<double> getPriceByClass(int aircraftId, Class cl);

    }
}
