using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;

namespace EPTS.Repositories.WebServices.WebAPI.Services.Interfaces
{
    public interface ITestLinkService
    {
        Task<bool> CreateTestLink(TestLink entity);
        Task<bool> DeleteTestLink(Guid id);
        Task<IEnumerable<TestLink>> GetAllTestLinks(string whereValue, string orderBy);
        Task<bool> UpdateTestLink(TestLink entity);
        Task<TestLink> GetTestLinkById(Guid id);

    }
}
