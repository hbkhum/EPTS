using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;

namespace EPTS.Repositories.WebServices.WebAPI.Services.Interfaces
{
    public interface IPartNumberService
    {
        Task<bool> CreatePartNumber(PartNumber entity);
        Task<bool> DeletePartNumber(Guid id);
        Task<IEnumerable<PartNumber>> GetAllPartNumbers(string whereValue, string orderBy);
        Task<bool> UpdatePartNumber(PartNumber entity);
        Task<PartNumber> GetPartNumberById(Guid id);
    }
}