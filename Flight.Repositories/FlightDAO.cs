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
    public class FlightDAO : IFlightDAO
    {
        private readonly FullStackDbContext _db;
        public FlightDAO(FullStackDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Flight>?> GetAllAsync()
        {
            return await _db.Flights
                .Include(f => f.AircraftAircraft)
                .Include(f => f.DepartureAirport)
                .Include(f => f.ArrivalAirport)
                .ToListAsync();
        }




        private async Task<bool> IsClassAvailableAsync(int aircraftId, int selectedClassId, int requiredSeats)
        {
            var availableSeats = await _db.Seats
                .Where(s => s.AircraftId == aircraftId &&
                            s.ClassId == selectedClassId &&
                            s.IsOccupied == false)
                .CountAsync();

            return availableSeats >= requiredSeats;
        }

        public async Task<Flight> getFlightById(int id)
        {
            try
            {
                return await _db.Flights
                    .Where(f => f.FlightId == id)
                    .Include(f => f.DepartureAirport)
                    .Include(f => f.ArrivalAirport)
                    .Include(f => f.AircraftAircraft)
                    .FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Error fetching flight by ID", e);
            }
        }

        public async Task<List<List<Flight>>> GetFlightsFromTwoAirports(int departureAirportId, int arrivalAirportId)
        {
            try
            {
                var routes = await _db.Routes
                    .Where(r => r.DepartureAirportId == departureAirportId && r.ArrivalAirportId == arrivalAirportId)
                    .ToListAsync();

                var flights = await _db.Flights
                    .Include(f => f.AircraftAircraft)
                    .Include(f => f.DepartureAirport)
                    .Include(f => f.ArrivalAirport)
                    .Where(f => f.DepartureTime.HasValue)
                    .ToListAsync();

                var trips = new List<List<Flight>>();

                foreach (var route in routes)
                {
                    // Direct
                    if (route.TransferAirportId1 == null && route.TransferAirportId2 == null)
                    {
                        var directFlights = flights
                            .Where(f =>
                                f.DepartureAirportId == route.DepartureAirportId &&
                                f.ArrivalAirportId == route.ArrivalAirportId)
                            .ToList();

                        foreach (var directFlight in directFlights)
                        {
                            trips.Add(new List<Flight> { directFlight });
                        }
                    }
                    // 1 overstap
                    else if (route.TransferAirportId1 != null && route.TransferAirportId2 == null)
                    {
                        var firstLegs = flights
                            .Where(f =>
                                f.DepartureAirportId == route.DepartureAirportId &&
                                f.ArrivalAirportId == route.TransferAirportId1)
                            .ToList();

                        var secondLegs = flights
                            .Where(f =>
                                f.DepartureAirportId == route.TransferAirportId1 &&
                                f.ArrivalAirportId == route.ArrivalAirportId)
                            .ToList();

                        foreach (var first in firstLegs)
                        {
                            foreach (var second in secondLegs)
                            {
                                trips.Add(new List<Flight> { first, second });
                            }
                        }
                    }
                    // 2 overstappen
                    else if (route.TransferAirportId1 != null && route.TransferAirportId2 != null)
                    {
                        var firstLegs = flights
                            .Where(f =>
                                f.DepartureAirportId == route.DepartureAirportId &&
                                f.ArrivalAirportId == route.TransferAirportId1)
                            .ToList();

                        var secondLegs = flights
                            .Where(f =>
                                f.DepartureAirportId == route.TransferAirportId1 &&
                                f.ArrivalAirportId == route.TransferAirportId2)
                            .ToList();

                        var thirdLegs = flights
                            .Where(f =>
                                f.DepartureAirportId == route.TransferAirportId2 &&
                                f.ArrivalAirportId == route.ArrivalAirportId)
                            .ToList();

                        foreach (var first in firstLegs)
                        {
                            foreach (var second in secondLegs)
                            {
                                foreach (var third in thirdLegs)
                                {
                                    trips.Add(new List<Flight> { first, second, third });
                                }
                            }
                        }
                    }
                }

                return trips
                    .OrderBy(t => t.FirstOrDefault()?.DepartureTime)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;

            }
        }
            public async Task<List<List<Flight>>> GetAvailableFlights(int departureAirportId, int arrivalAirportId, int selectedClassId, int requiredSeats, DateOnly? targetDate)
            {
            try
            {
                var routes = await _db.Routes
                    .Where(r => r.DepartureAirportId == departureAirportId && r.ArrivalAirportId == arrivalAirportId)
                    .ToListAsync();

                var flights = await _db.Flights
                    .Include(f => f.AircraftAircraft)
                    .Include(f => f.DepartureAirport)
                    .Include(f => f.ArrivalAirport)
                    .ToListAsync();

                var availableTrips = new List<List<Flight>>();

                foreach (var route in routes)
                {
                    // Direct flights (zonder overstappen)
                    if (route.TransferAirportId1 == null && route.TransferAirportId2 == null)
                    {
                        var directFlights = flights
                            .Where(f =>
                                f.DepartureAirportId == route.DepartureAirportId &&
                                f.ArrivalAirportId == route.ArrivalAirportId &&
                                f.DepartureTime.HasValue &&
                                f.DepartureTime.Value.Date == targetDate.Value.ToDateTime(TimeOnly.MinValue).Date)
                            .ToList();

                        foreach (var directFlight in directFlights)
                        {
                            if (await IsClassAvailableAsync(directFlight.AircraftAircraft.AircraftId, selectedClassId, requiredSeats))
                            {
                                availableTrips.Add(new List<Flight> { directFlight });
                            }
                        }
                    }
                    // 1 overstap
                    else if (route.TransferAirportId1 != null && route.TransferAirportId2 == null)
                    {
                        var firstLegs = flights
                            .Where(f =>
                                f.DepartureAirportId == route.DepartureAirportId &&
                                f.ArrivalAirportId == route.TransferAirportId1 &&
                                f.DepartureTime.HasValue &&
                                f.DepartureTime.Value.Date == targetDate.Value.ToDateTime(TimeOnly.MinValue).Date)
                            .ToList();

                        var secondLegs = flights
                            .Where(f =>
                                f.DepartureAirportId == route.TransferAirportId1 &&
                                f.ArrivalAirportId == route.ArrivalAirportId &&
                                f.DepartureTime.HasValue &&
                                f.DepartureTime.Value.Date == targetDate.Value.ToDateTime(TimeOnly.MinValue).Date)
                            .ToList();

                        foreach (var firstLeg in firstLegs)
                        {
                            foreach (var secondLeg in secondLegs)
                            {
                                if (await IsClassAvailableAsync(firstLeg.AircraftAircraft.AircraftId, selectedClassId, requiredSeats) &&
                                    await IsClassAvailableAsync(secondLeg.AircraftAircraft.AircraftId, selectedClassId, requiredSeats))
                                {
                                    availableTrips.Add(new List<Flight> { firstLeg, secondLeg });
                                }
                            }
                        }
                    }
                    // 2 overstappen
                    else if (route.TransferAirportId1 != 0 && route.TransferAirportId2 != 0)
                    {
                        var firstLegs = flights
                            .Where(f =>
                                f.DepartureAirportId == route.DepartureAirportId &&
                                f.ArrivalAirportId == route.TransferAirportId1 &&
                                f.DepartureTime.HasValue &&
                                f.DepartureTime.Value.Date == targetDate.Value.ToDateTime(TimeOnly.MinValue).Date)
                            .ToList();

                        var secondLegs = flights
                            .Where(f =>
                                f.DepartureAirportId == route.TransferAirportId1 &&
                                f.ArrivalAirportId == route.TransferAirportId2 &&
                                f.DepartureTime.HasValue &&
                                f.DepartureTime.Value.Date == targetDate.Value.ToDateTime(TimeOnly.MinValue).Date)
                            .ToList();

                        var thirdLegs = flights
                            .Where(f =>
                                f.DepartureAirportId == route.TransferAirportId2 &&
                                f.ArrivalAirportId == route.ArrivalAirportId &&
                                f.DepartureTime.HasValue &&
                                f.DepartureTime.Value.Date == targetDate.Value.ToDateTime(TimeOnly.MinValue).Date)
                            .ToList();

                        foreach (var firstLeg in firstLegs)
                        {
                            foreach (var secondLeg in secondLegs)
                            {
                                foreach (var thirdLeg in thirdLegs)
                                {
                                    if (await IsClassAvailableAsync(firstLeg.AircraftAircraft.AircraftId, selectedClassId, requiredSeats) &&
                                        await IsClassAvailableAsync(secondLeg.AircraftAircraft.AircraftId, selectedClassId, requiredSeats) &&
                                        await IsClassAvailableAsync(thirdLeg.AircraftAircraft.AircraftId, selectedClassId, requiredSeats))
                                    {
                                        availableTrips.Add(new List<Flight> { firstLeg, secondLeg, thirdLeg });
                                    }
                                }
                            }
                        }
                    }
                }

                return availableTrips
                    .OrderBy(flightGroup => flightGroup.FirstOrDefault()?.DepartureTime)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }



    }
}
