﻿using FlightProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Services.Interfaces
{
    public interface IBookingService
    {
        Task<int> Book(double TotalPrice, string UserId);
        Task<Booking> GetBookingById(int id);
        Task<List<Booking>> GetAllBookingsByUser(string UserId);

    }
}
