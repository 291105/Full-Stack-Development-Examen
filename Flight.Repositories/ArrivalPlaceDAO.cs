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
    public class ArrivalPlaceDAO : IDAO<ArrivalPlace>
    {
        private readonly FullStackDbContext _db;
        public ArrivalPlaceDAO(FullStackDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<ArrivalPlace>?> GetAllAsync()
        {
            return await _db.ArrivalPlaces.Include(dp => dp.Place).ToListAsync();
        }
    }
}
