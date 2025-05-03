using FlightProject.Domain.Data;

namespace FlightProject.Services.Interfaces
{
    public interface IHotelsApiService
    {
        public Task<List<Hotel>> GetHotelsAsync(string city);
    }
}
