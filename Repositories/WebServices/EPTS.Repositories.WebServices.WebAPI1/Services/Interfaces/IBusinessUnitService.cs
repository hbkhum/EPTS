using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;

namespace EPTS.Repositories.WebServices.WebAPI.Services.Interfaces
{
    public interface IBusinessUnitService
    {
        Task<bool> CreateBusinessUnit(BusinessUnit entity);
        Task<bool> DeleteBusinessUnit(Guid id);
        Task<IEnumerable<BusinessUnit>> GetAllBusinessUnits(string whereValue, string orderBy);
        Task<bool> UpdateBusinessUnit(BusinessUnit entity);
        Task<BusinessUnit> GetBusinessUnitById(Guid id);

    }
}