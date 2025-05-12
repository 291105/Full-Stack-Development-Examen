using FlightProject.Domain.Data;
using FlightProject.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Repositories
{
    public class SeatDAO : ISeatDAO
    {
        private readonly FullStackDbContext _db;

        public SeatDAO(FullStackDbContext db)
        {
            _db = db;
        }

        public async Task<string> GetSeatNumberBySeatId(int id)
        {
            var seat = await _db.Seats.Where(s => s.SeatId == id).FirstOrDefaultAsync();
            return seat.SeatNumber;
        }
    }
}
