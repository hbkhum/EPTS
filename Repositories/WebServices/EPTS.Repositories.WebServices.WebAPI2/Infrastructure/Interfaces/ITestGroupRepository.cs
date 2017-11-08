using System;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Core;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Interfaces
{
    public interface ITestGroupRepository : IGenericRepositoryEntities<TestGroup>
    {
        /// <summary>
        /// Get the Test Group By Id
        /// </summary>
        /// <param name="id">Is the Id from the database</param>
        /// <returns>Return a object type Station</returns>
        Task<TestGroup> GetById(Guid id);
    }
}