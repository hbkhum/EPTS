using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;

namespace EPTS.Repositories.WebServices.WebAPI.Services.Interfaces
{
    public interface ITestService
    {
        Task<bool> CreateTest(Test entity);
        Task<bool> DeleteTest(Guid id);
        Task<IEnumerable<Test>> GetAllTests(string whereValue, string orderBy);
        Task<bool> UpdateTest(Test entity);
        Task<Test> GetTestById(Guid id);
        Task<IEnumerable<Test>> GetAllTestByTestGroupId(Guid testgroupid);

    }
}
