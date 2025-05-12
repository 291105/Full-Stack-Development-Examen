using FlightProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Repositories.Interfaces
{
    public interface IMealDAO
    {
        Task<List<Meal>> GetAllMeals(string departureAirport);
        Task<Meal> GetMealById(int id);

        Task<int> GetMealIdByMealName(string mealName);

    }
}
