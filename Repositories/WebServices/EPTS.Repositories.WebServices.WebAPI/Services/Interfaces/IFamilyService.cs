using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;

namespace EPTS.Repositories.WebServices.WebAPI.Services.Interfaces
{
    public interface IFamilyService
    {
        Task<bool> CreateFamily(Family entity);
        Task<bool> DeleteFamily(Guid id);
        Task<IEnumerable<Family>> GetAllFamilies(string whereValue, string orderBy);
        Task<bool> UpdateFamily(Family entity);
        Task<Family> GetFamilyById(Guid id);

    }
}