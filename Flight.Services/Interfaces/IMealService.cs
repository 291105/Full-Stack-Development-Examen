using FlightProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Services.Interfaces
{
    public interface IMealService
    {
        Task<List<Meal>> GetAllMeals(string departureAirport);
        Task<Meal> GetMealById(int id);
    }
}
