using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;

namespace EPTS.Repositories.WebServices.WebAPI.Services.Interfaces
{
    public interface ITestPlanLinkService
    {
        Task<bool> CreateTestPlanLink(TestPlanLink entity);
        Task<bool> DeleteTestPlanLink(Guid id);
        Task<IEnumerable<TestPlanLink>> GetAllTestPlanLinks(string whereValue, string orderBy);
        Task<bool> UpdateTestPlanLink(TestPlanLink entity);
        Task<TestPlanLink> GetTestPlanLinkById(Guid id);

    }
}
