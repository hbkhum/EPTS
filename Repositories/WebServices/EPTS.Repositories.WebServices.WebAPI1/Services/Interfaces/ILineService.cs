using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;

namespace EPTS.Repositories.WebServices.WebAPI.Services.Interfaces
{
    public interface ILineService
    {
        Task<bool> CreateLine(Line entity);
        Task<bool> DeleteLine(Guid id);
        Task<IEnumerable<Line>> GetAllLines(string whereValue, string orderBy);
        Task<bool> UpdateLine(Line entity);
        Task<Line> GetLineById(Guid id);
    }
}
