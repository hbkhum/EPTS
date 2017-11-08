using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;

namespace EPTS.Repositories.WebServices.WebAPI.Services.Interfaces
{
    public interface ITestTypeService
    {
        Task<bool> CreateTestType(TestType entity);
        Task<bool> DeleteTestType(Guid id);
        Task<IEnumerable<TestType>> GetAllTestTypes(string whereValue, string orderBy);
        Task<bool> UpdateTestType(TestType entity);
        Task<TestType> GetTestTypeById(Guid id);

    }
}
