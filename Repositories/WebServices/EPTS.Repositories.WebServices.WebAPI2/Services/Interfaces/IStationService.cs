using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;

namespace EPTS.Repositories.WebServices.WebAPI.Services.Interfaces
{
    public interface IStationService
    {
        Task<bool> CreateStation(Station entity);
        Task<bool> DeleteStation(Guid id);
        Task<IEnumerable<Station>> GetAllStations(string whereValue, string orderBy);
        Task<bool> UpdateStation(Station entity);
        Task<Station> GetStationById(Guid id);

    }
}