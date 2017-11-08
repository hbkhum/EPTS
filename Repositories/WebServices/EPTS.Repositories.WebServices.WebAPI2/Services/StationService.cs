using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Repositories;
using EPTS.Repositories.WebServices.WebAPI.Services.Interfaces;

namespace EPTS.Repositories.WebServices.WebAPI.Services
{
    public class StationService : IStationService
    {

        private readonly IDataRepositories _dataRepositories;

        public StationService(IDataRepositories dataRepositories)
        {
            _dataRepositories = dataRepositories;
        }

        public async Task<bool> CreateStation(Station entity)
        {
            try
            {
                var result = false;
                _dataRepositories.StationRepository.Add(entity);
                _dataRepositories.StationRepository.Save();
                if (entity.StationId != Guid.NewGuid()) result = true;
                return await  Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteStation(Guid id)
        {
            try
            {
                var entity = await _dataRepositories.StationRepository.GetById(id);
                _dataRepositories.StationRepository.Delete(entity);
                _dataRepositories.StationRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<Station>> GetAllStations(string whereValue, string orderBy)
        {
            return await _dataRepositories.StationRepository.GetAllAsync(whereValue, orderBy);
        }

        public async Task<bool> UpdateStation(Station entity)
        {
            try
            {
                _dataRepositories.StationRepository.Edit(entity);
                _dataRepositories.StationRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Station> GetStationById(Guid id)
        {
            return await _dataRepositories.StationRepository.GetById(id);
                                 
        }
    }
}
