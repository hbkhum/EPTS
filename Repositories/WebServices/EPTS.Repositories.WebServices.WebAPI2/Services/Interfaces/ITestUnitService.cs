using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;

namespace EPTS.Repositories.WebServices.WebAPI.Services.Interfaces
{
    public interface ITestUnitService
    {
        Task<bool> CreateTestUnit(TestUnit entity);
        Task<bool> DeleteTestUnit(Guid id);
        Task<IEnumerable<TestUnit>> GetAllTestUnits(string whereValue, string orderBy);
        Task<bool> UpdateTestUnit(TestUnit entity);
        Task<TestUnit> GetTestUnitById(Guid id);

    }
}
