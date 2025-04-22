using FlightProject.Domain.Data;
using FlightProject.Domain.Data;
using FlightProject.Domain.Entities;
using FlightProject.Domain.Entities;
using FlightProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Repositories
{
    public class FlightDAO :  IFlightDAO
    {
        private readonly FullStackDbContext _db;
        public FlightDAO(FullStackDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Flight>?> GetAllAsync()
        {
            return await _db.Flights.ToListAsync();
        }

        public async Task<List<Flight>> GetFlightsFromTwoAirports(int airportID1, int airportID2)
        {
            try
            {
                return await _db.Flights
                    .Where(a => a.DepartureId == airportID1 && a.ArrivalId == airportID2)
                    .Include(f => f.Departure.Place)
                    .Include(f => f.Arrival.Place)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw; 
            }
        }

        public async Task<Flight> getFlightById(int id)
        {
            try
            {
                return await _db.Flights.Where(a => a.FlightId == id).Include(f => f.Departure.Place)
                    .Include(f => f.Arrival.Place).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception("error flights by id");
            }
        }
    }
}
