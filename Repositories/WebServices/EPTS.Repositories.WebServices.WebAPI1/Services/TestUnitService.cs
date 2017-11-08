using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;
using EPTS.Repositories.WebServices.WebAPI.Repositories;
using EPTS.Repositories.WebServices.WebAPI.Services.Interfaces;

namespace EPTS.Repositories.WebServices.WebAPI.Services
{
    public class TestUnitService : ITestUnitService
    {

        private readonly IDataRepositories _dataRepositories;

        public TestUnitService(IDataRepositories dataRepositories)
        {
            _dataRepositories = dataRepositories;
        }

        public async Task<bool> CreateTestUnit(TestUnit entity)
        {
            try
            {
                var result = false;
                _dataRepositories.TestUnitRepository.Add(entity);
                _dataRepositories.TestUnitRepository.Save();
                if (entity.TestUnitId != Guid.NewGuid()) result = true;
                //entity.BusinessUnit =
                //    Task.Run(async () => await _dataRepositories.BusinessUnitRepository.GetById(entity.BusinessUnitId))
                //        .Result;
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public async Task<bool> DeleteTestUnit(Guid id)
        {
            try
            {
                var entity = await _dataRepositories.TestUnitRepository.GetById(id);
                _dataRepositories.TestUnitRepository.Delete(entity);
                _dataRepositories.TestUnitRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<IEnumerable<TestUnit>> GetAllTestUnits(string whereValue, string orderBy)
        {
            var result= await _dataRepositories.TestUnitRepository.GetAllAsync(whereValue, orderBy);
            result = result.Select(c => new TestUnit
            {
                TestUnitId = c.TestUnitId,
                TestUnitName = c.TestUnitName,
                //BusinessUnitId = c.BusinessUnitId,
                //BusinessUnit = Task.Run(async () => await _dataRepositories.BusinessUnitRepository.GetById(c.BusinessUnitId)).Result,
            }).ToList();
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateTestUnit(TestUnit entity)
        {
            try
            {
                _dataRepositories.TestUnitRepository.Edit(entity);
                _dataRepositories.TestUnitRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<TestUnit> GetTestUnitById(Guid id)
        {
            return await  _dataRepositories.TestUnitRepository.GetById(id);
        }
    }
}

