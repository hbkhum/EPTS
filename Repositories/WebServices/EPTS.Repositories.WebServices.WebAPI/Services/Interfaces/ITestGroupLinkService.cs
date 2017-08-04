using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;

namespace EPTS.Repositories.WebServices.WebAPI.Services.Interfaces
{
    public interface ITestGroupLinkService
    {
        Task<bool> CreateTestGroupLink(TestGroupLink entity);
        Task<bool> DeleteTestGroupLink(Guid id);
        Task<IEnumerable<TestGroupLink>> GetAllTestGroupLinks(string whereValue, string orderBy);
        Task<bool> UpdateTestGroupLink(TestGroupLink entity);
        Task<TestGroupLink> GetTestGroupLinkById(Guid id);

    }
}
