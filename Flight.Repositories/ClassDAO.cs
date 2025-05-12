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
    public class ClassDAO : IClassDAO
    {
        private readonly FullStackDbContext _db;
        public ClassDAO(FullStackDbContext db)
        {
            _db = db;
        }
        

        public async Task<IEnumerable<Class>?> GetAllAsync()
        {
            return await _db.Classes.ToListAsync();
        }

        public async Task<Class> getClassById(int id)
        {
            return await _db.Classes.Where(cl => cl.ClassId == id).FirstOrDefaultAsync();
        }

        public async Task<int> getClassIdByClassName(string className)
        {
            return await _db.Classes.Where(cl => cl.Name == className).Select(cl => cl.ClassId).FirstOrDefaultAsync();
        }
    }
}
