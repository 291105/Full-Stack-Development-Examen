using FlightProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Repositories.Interfaces
{
    public interface IHotelsApiDAO
    {
        public Task<List<Hotel>> GetHotelsAsync(string city);
    }
}
