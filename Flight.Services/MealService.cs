using FlightProject.Domain.Entities;
using FlightProject.Repositories.Interfaces;
using FlightProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Services
{
    public class MealService : IMealService
    {
        private readonly IMealDAO _mealDAO;

        public MealService(IMealDAO mealDAO)
        {
            _mealDAO = mealDAO;
        }

        public async Task<List<Meal>> GetAllMeals(string departureAirport)
        {
            return await _mealDAO.GetAllMeals(departureAirport);
        }

        public async Task<Meal> GetMealById(int id)
        {
            return await _mealDAO.GetMealById(id);
        }

        public async Task<int> GetMealIdByMealName(string mealName)
        {
            return await _mealDAO.GetMealIdByMealName(mealName);
        }
    }
}
