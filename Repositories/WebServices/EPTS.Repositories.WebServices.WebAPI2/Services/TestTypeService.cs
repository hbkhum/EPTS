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
    public class TestTypeService : ITestTypeService
    {

        private readonly IDataRepositories _dataRepositories;

        public TestTypeService(IDataRepositories dataRepositories)
        {
            _dataRepositories = dataRepositories;
        }

        public async Task<bool> CreateTestType(TestType entity)
        {
            try
            {
                var result = false;
                _dataRepositories.TestTypeRepository.Add(entity);
                _dataRepositories.TestTypeRepository.Save();
                if (entity.TestTypeId != Guid.NewGuid()) result = true;
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

        public async Task<bool> DeleteTestType(Guid id)
        {
            try
            {
                var entity = await _dataRepositories.TestTypeRepository.GetById(id);
                _dataRepositories.TestTypeRepository.Delete(entity);
                _dataRepositories.TestTypeRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<IEnumerable<TestType>> GetAllTestTypes(string whereValue, string orderBy)
        {
            var result= await _dataRepositories.TestTypeRepository.GetAllAsync(whereValue, orderBy);
            result = result.Select(c => new TestType
            {
                TestTypeId = c.TestTypeId,
                TestTypeName = c.TestTypeName,
                //BusinessUnitId = c.BusinessUnitId,
                //BusinessUnit = Task.Run(async () => await _dataRepositories.BusinessUnitRepository.GetById(c.BusinessUnitId)).Result,
            }).ToList();
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateTestType(TestType entity)
        {
            try
            {
                _dataRepositories.TestTypeRepository.Edit(entity);
                _dataRepositories.TestTypeRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<TestType> GetTestTypeById(Guid id)
        {
            return await  _dataRepositories.TestTypeRepository.GetById(id);
        }
    }
}

