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
    public class ClassService : IClassService
    {
        private readonly IClassDAO _classDao;
        public ClassService(IClassDAO classDao) {
            _classDao = classDao;
        }
        public async Task<IEnumerable<Class>?> GetAllAsync()
        {
            return await _classDao.GetAllAsync();
        }

        public async Task<Class> getClassById(int id)
        {
            return await _classDao.getClassById(id);
        }

        public async Task<int> getClassIdByClassName(string className)
        {
            return await _classDao.getClassIdByClassName(className);
        }
    }
}
