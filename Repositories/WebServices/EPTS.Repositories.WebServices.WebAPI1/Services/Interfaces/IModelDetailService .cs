using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;

namespace EPTS.Repositories.WebServices.WebAPI.Services.Interfaces
{
    public interface IModelDetailService
    {
        Task<bool> CreateModelDetail(ModelDetail entity);
        Task<bool> DeleteModelDetail(Guid id);
        Task<IEnumerable<ModelDetail>> GetAllModelDetails(string whereValue, string orderBy);
        Task<bool> UpdateModelDetail(ModelDetail entity);
        Task<ModelDetail> GetModelDetailById(Guid id);
    }
}