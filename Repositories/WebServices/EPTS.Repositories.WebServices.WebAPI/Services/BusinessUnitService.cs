using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Repositories;
using EPTS.Repositories.WebServices.WebAPI.Services.Interfaces;

namespace EPTS.Repositories.WebServices.WebAPI.Services
{
    public class BusinessUnitService : IBusinessUnitService
    {

        private readonly IDataRepositories _dataRepositories;

        public BusinessUnitService(IDataRepositories dataRepositories)
        {
            _dataRepositories = dataRepositories;
        }

        public async Task<bool> CreateBusinessUnit(BusinessUnit entity)
        {
            
            try
            {
                var result = false;
                _dataRepositories.BusinessUnitRepository.Add(entity);
                _dataRepositories.BusinessUnitRepository.Save();
                if (entity.BusinessUnitId != Guid.NewGuid()) result = true;
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public async Task<bool> DeleteBusinessUnit(Guid id)
        {
            var entity = await _dataRepositories.BusinessUnitRepository.GetById(id);
            try
            {
                _dataRepositories.BusinessUnitRepository.Delete(entity);
                _dataRepositories.BusinessUnitRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public async Task<IEnumerable<BusinessUnit>> GetAllBusinessUnits(string whereValue, string orderBy)
        {
            return await _dataRepositories.BusinessUnitRepository.GetAllAsync(whereValue, orderBy);
        }

        public async Task<bool> UpdateBusinessUnit(BusinessUnit entity)
        {
            try
            {
                _dataRepositories.BusinessUnitRepository.Edit(entity);
                _dataRepositories.BusinessUnitRepository.Save();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return await Task.FromResult(true);
        }

        public async Task<BusinessUnit> GetBusinessUnitById(Guid id)
        {
            return await _dataRepositories.BusinessUnitRepository.GetById(id);
        }
    }
}
