using System;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Core;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Interfaces
{
    public interface IModelRepository : IGenericRepositoryEntities<Model>
    {
        /// <summary>
        /// Get the Model By Id
        /// </summary>
        /// <param name="id">Is the Id from the database</param>
        /// <returns>Return a object type Model</returns>
        Task<Model> GetById(Guid id);

    }
}
