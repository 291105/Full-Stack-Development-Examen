using FlightProject.Domain.Data;
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
    
    public class AirportDAO : IAirportDAO
    {
        private readonly FullStackDbContext _db;
        public AirportDAO(FullStackDbContext db)
        {
            _db = db;
        }

        public async Task<List<Airport>> GetAllArrivalAirports()
        {
            try
            {
                // Haal alle routes op en koppel de arrival airports zonder te filteren op een specifiek departure airport
                var routes = await _db.Routes
                    .Include(r => r.ArrivalAirport)  // Include de ArrivalAirport
                    .ToListAsync();

                // Haal de arrival luchthavens op uit de routes
                var arrivalAirports = routes
                    .Select(r => r.ArrivalAirport)
                    .Distinct()
                    .ToList();

                return arrivalAirports;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Error fetching arrival airports");
            }
        }

        public async Task<List<Airport>> GetAllDepartureAirports()
        {
            try
            {
                // Haal alle routes op en koppel de departure airports zonder te filteren op een specifiek arrival airport
                var routes = await _db.Routes
                    .Include(r => r.DepartureAirport)  // Include de DepartureAirport
                    .ToListAsync();

                // Haal de departure luchthavens op uit de routes
                var departureAirports = routes
                    .Select(r => r.DepartureAirport)
                    .Distinct()
                    .ToList();

                return departureAirports;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Error fetching departure airports");
            }
        }

        // Haal alle luchthavens op (standaard implementatie)
        public async Task<IEnumerable<Airport>?> GetAllAsync()
        {
            try
            {
                return await _db.Airports.ToListAsync();  // Haal alle luchthavens op uit de database
            }
            catch (Exception ex)
            {
                // Log fout en gooi opnieuw
                Console.WriteLine(ex.Message);
                throw new Exception("Error fetching all airports");
            }
        }
    }
}
