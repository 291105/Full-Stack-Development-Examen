using FlightProject.Repositories.Interfaces;
using FlightProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Services
{
    public class SeatService : ISeatService
    {
        private readonly ISeatDAO _seatDAO;

        public SeatService(ISeatDAO seatDAO)
        {
            _seatDAO = seatDAO;
        }

        public async Task<string> GetSeatNumberBySeatId(int id)
        {
            return await _seatDAO.GetSeatNumberBySeatId(id);
        }
    }
}
