using System;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Core;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Interfaces
{
    public interface IStationGroupsRepository : IGenericRepositoryEntities<StationGroup>
    {
        /// <summary>
        /// Get the Station By Id
        /// </summary>
        /// <param name="id">Is the Id from the database</param>
        /// <returns>Return a object type Station</returns>
        Task<StationGroup> GetById(Guid id);

    }
}