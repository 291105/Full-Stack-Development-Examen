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
    
    public class AirportDAO : IDAO<Place>
    {
        private readonly FullStackDbContext _db;
        public AirportDAO(FullStackDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Place>?> GetAllAsync()
        {
            try
            {
                return await _db.Places.ToListAsync();
            }catch (Exception ex) {
                Console.WriteLine("error in DAO GetAllAsync");
                throw;
            }
        }
    }
}
