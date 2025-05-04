using FlightProject.Domain;
using FlightProject.Repositories;
using FlightProject.Repositories.Interfaces;
using FlightProject.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace FlightProject.Services
{
    public class HotelsApiService : IHotelsApiService
    {
        private readonly IHotelsApiDAO _hotelsApiDAO;

        public HotelsApiService(IHotelsApiDAO hotelsApiDAO)
        {
            _hotelsApiDAO = hotelsApiDAO;
        }

        public async Task<List<Hotel>> GetHotelsAsync(string city)
        {
            return await _hotelsApiDAO.GetHotelsAsync(city);
        }
    }
}
