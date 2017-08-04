using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;

namespace EPTS.Repositories.WebServices.WebAPI.Services.Interfaces
{
    public interface IModelService
    {
        Task<bool> CreateModel(Model entity);
        Task<bool> DeleteModel(Guid id);
        Task<IEnumerable<Model>> GetAllModels(string whereValue, string orderBy);
        Task<bool> UpdateModel(Model entity);
        Task<Model> GetModelById(Guid id);
    }
}