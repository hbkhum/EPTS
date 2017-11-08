using System;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Core;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Interfaces
{
    public interface IPartNumberRepository : IGenericRepositoryEntities<PartNumber>
    {
        /// <summary>
        /// Get the PartNumber By Id
        /// </summary>
        /// <param name="id">Is the Id from the database</param>
        /// <returns>Return a object type PartNumer</returns>
        Task<PartNumber> GetById(Guid id);

    }
}
