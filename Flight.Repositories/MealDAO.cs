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
    public class MealDAO : IDAO<Meal>
    {
        private readonly FullStackDbContext _db;
        public MealDAO(FullStackDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Meal>?> GetAllAsync()
        {
            return await _db.Meals.ToListAsync();
        }
    }
}
