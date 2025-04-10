using FlightProject.Domain.DataDB;
using FlightProject.Domain.EntitiesDB;
using FlightProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Repositories
{
    public class DeparturePlaceDAO : IDAO<DeparturePlace>
    {
        private readonly FullStackDbContext _db;
        public DeparturePlaceDAO(FullStackDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<DeparturePlace>?> GetAllAsync()
        {
            return await _db.DeparturePlaces.Include(dp => dp.Place).ToListAsync();
        }
    }
}
