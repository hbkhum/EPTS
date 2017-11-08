using System;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Core;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;

namespace EPTS.Repositories.WebServices.WebAPI.Infrastructure.Interfaces
{
    public interface ILineRepository : IGenericRepositoryEntities<Line>
    {
        /// <summary>
        /// Get the Line By Id
        /// </summary>
        /// <param name="id">Is the Id from the database</param>
        /// <returns>Return a object type Line</returns>
        Task<Line> GetById(Guid id);

    }
}
