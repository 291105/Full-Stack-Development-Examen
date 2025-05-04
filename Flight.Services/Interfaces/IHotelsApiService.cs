using FlightProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Services.Interfaces
{
    public interface IHotelsApiService
    {
        public Task<List<Hotel>> GetHotelsAsync(string city);
    }
}
