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
    public class MealService : IService<Meal>
    {
        private readonly IDAO<Meal> _mealDAO;

        public MealService(IDAO<Meal> mealDAO)
        {
            _mealDAO = mealDAO;
        }
        public async Task<IEnumerable<Meal>?> GetAllAsync()
        {
            return await _mealDAO.GetAllAsync();
        }
    }
}
