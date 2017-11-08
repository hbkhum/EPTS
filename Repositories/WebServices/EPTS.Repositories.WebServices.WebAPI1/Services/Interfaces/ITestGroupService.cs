using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;

namespace EPTS.Repositories.WebServices.WebAPI.Services.Interfaces
{
    public interface ITestGroupService
    {
        Task<bool> CreateTestGroup(TestGroup entity);
        Task<bool> DeleteTestGroup(Guid id);
        Task<IEnumerable<TestGroup>> GetAllTestGroups(string whereValue, string orderBy);
        Task<bool> UpdateTestGroup(TestGroup entity);
        Task<TestGroup> GetTestGroupById(Guid id);
        Task<IEnumerable<TestGroup>> GetAllTestGroupByTestPlanId(Guid testplanid);

    }
}
