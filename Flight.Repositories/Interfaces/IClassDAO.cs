using FlightProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Repositories.Interfaces
{
    public interface IClassDAO : IDAO<Class>
    {
        Task<Class> getClassById(int id);
        Task<int> getClassIdByClassName(string className);
    }
}
