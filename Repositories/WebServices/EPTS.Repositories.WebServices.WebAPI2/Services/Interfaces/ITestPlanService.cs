using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;

namespace EPTS.Repositories.WebServices.WebAPI.Services.Interfaces
{
    public interface ITestPlanService
    {
        Task<bool> CreateTestPlan(TestPlan entity);
        Task<bool> DeleteTestPlan(Guid id);
        Task<IEnumerable<TestPlan>> GetAllTestPlans(string whereValue, string orderBy);
        Task<bool> UpdateTestPlan(TestPlan entity);
        Task<TestPlan> GetTestPlanById(Guid id);

    }
}
