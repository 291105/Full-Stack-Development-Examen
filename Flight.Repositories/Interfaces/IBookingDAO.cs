using FlightProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Repositories.Interfaces
{
    public interface IBookingDAO
    {
        Task<int> Book(double TotalPrice, string UserId);

        Task<Booking> GetBookingById(int id);
    }
}
