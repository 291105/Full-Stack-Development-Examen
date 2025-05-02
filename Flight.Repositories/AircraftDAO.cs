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
    public class AircraftDAO : IAircraftDAO
    {
        private readonly FullStackDbContext _db;
        public AircraftDAO(FullStackDbContext db)
        {
            _db = db;
        }
        public async Task<double> getPriceByClass(int aircraftId, Class cl)
        {
            // Eerst de classnaam verkrijgen
            var classname = cl.Name;

            // Zoek de aircraft op basis van het ID
            var aircraft = _db.Aircraft.FirstOrDefault(a => a.AircraftId == aircraftId);

            if (aircraft == null)
            {
                // Vliegtuig niet gevonden, dus een exception of een foutmelding teruggeven
                throw new InvalidOperationException("Aircraft not found");
            }

            // Nu de prijs ophalen op basis van de class
            double price = 0;

            switch (classname.ToLower())
            {
                case "economy":
                    price = aircraft.EconomyPrice;
                    break;
                case "business":
                    price = aircraft.BusinessPrice;
                    break;
                case "first class":
                    price = aircraft.FirstClassPrice;
                    break;
                default:
                    throw new InvalidOperationException("Unknown class type");
            }

            return price;
        }
    }
}
