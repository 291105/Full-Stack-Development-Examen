﻿using FlightProject.Domain.EntitiesDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Repositories.Interfaces
{
    public interface IFlightDAO : IDAO<Flight>
    {
        Task<List<Flight>> GetFlightsFromTwoAirports(int airportID1, int airportID2);
        
    }
}
