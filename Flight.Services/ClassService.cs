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
    public class ClassService : IService<Class>
    {
        private readonly IDAO<Class> _classDao;
        public ClassService(IDAO<Class> classDao) {
            _classDao = classDao;
        }
        public async Task<IEnumerable<Class>?> GetAllAsync()
        {
            return await _classDao.GetAllAsync();
        }
    }
}
