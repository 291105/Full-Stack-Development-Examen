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
    public class MealDAO : IMealDAO
    {
        private readonly FullStackDbContext _db;
        public MealDAO(FullStackDbContext db)
        {
            _db = db;
        }


        public async Task<List<Meal>> GetAllMeals(string departureAirport)
        {
            // Stap 1: Haal alle maaltijden op uit de database
            var meals = await _db.Meals.ToListAsync();

            // Lijst zonder lokaal geïnspireerde maaltijden
            var regularMeals = meals.Where(m => m.Kind != "Lokale maaltijd").ToList();

            // Lijst van lokaal geïnspireerde maaltijden
            var localMeals = meals.Where(m => m.Kind == "Lokale maaltijd").ToList();

            // Stap 2: Zoek de maaltijd in de lijst van lokaal geïnspireerde maaltijden op basis van bestemming
            var localMealForDestination = GetLocalMealFromList(localMeals, departureAirport);
            if (localMealForDestination != null)
            {
                regularMeals.Add(localMealForDestination);  // Voeg de gevonden maaltijd toe aan de reguliere lijst
            }

            return regularMeals;
        }

        

        // Methode die de juiste maaltijd zoekt op basis van de bestemming uit de lijst van lokaal geïnspireerde maaltijden
        private Meal? GetLocalMealFromList(List<Meal> localMeals, string bestemming)
        {
            // Zoek de maaltijd die past bij de bestemming
            switch (bestemming)
            {
                case "Londen":
                    return localMeals.FirstOrDefault(m => m.Name.Equals("Fish and Chips", StringComparison.OrdinalIgnoreCase));
                case "Tokio":
                    return localMeals.FirstOrDefault(m => m.Name.Equals("Sushi", StringComparison.OrdinalIgnoreCase));
                case "Sydney":
                    return localMeals.FirstOrDefault(m => m.Name.Equals("Meatpie of lamington", StringComparison.OrdinalIgnoreCase));
                case "Singapore":
                    return localMeals.FirstOrDefault(m => m.Name.Equals("Hainanese chicken rice", StringComparison.OrdinalIgnoreCase));
                case "Kaapstad":
                    return localMeals.FirstOrDefault(m => m.Name.Equals("Bobotie", StringComparison.OrdinalIgnoreCase));
                default:
                    return null;
            }
        }

        public async Task<Meal> GetMealById(int id)
        {
            return await _db.Meals.Where(cl => cl.MealId == id).FirstOrDefaultAsync();
        }

        public async Task<int> GetMealIdByMealName(string mealName)
        {
            return await _db.Meals.Where(m => m.Name == mealName).Select(m => m.MealId).FirstOrDefaultAsync();
        }
    }
}
