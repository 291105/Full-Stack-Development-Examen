using FlightProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Repositories.Interfaces
{
    public interface ISeatDAO
    {
        Task<string> GetSeatNumberBySeatId(int id);
    }
}
