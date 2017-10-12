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
    public class TestService : ITestService
    {

        private readonly IDataRepositories _dataRepositories;

        public TestService(IDataRepositories dataRepositories)
        {
            _dataRepositories = dataRepositories;
        }

        public async Task<bool> CreateTest(Test entity)
        {
            try
            {
                var result = false;
                _dataRepositories.TestRepository.Add(entity);
                _dataRepositories.TestRepository.Save();
                if (entity.TestId != Guid.NewGuid()) result = true;
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

        public async Task<bool> DeleteTest(Guid id)
        {
            try
            {
                var entity = await _dataRepositories.TestRepository.GetById(id);
                _dataRepositories.TestRepository.Delete(entity);
                _dataRepositories.TestRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<IEnumerable<Test>> GetAllTests(string whereValue, string orderBy)
        {
            var result= await _dataRepositories.TestRepository.GetAllAsync(whereValue, orderBy);
            result = result.Select(c => new Test
            {
                TestId = c.TestId,
                TestName = c.TestName,
                //BusinessUnitId = c.BusinessUnitId,
                //BusinessUnit = Task.Run(async () => await _dataRepositories.BusinessUnitRepository.GetById(c.BusinessUnitId)).Result,
            }).ToList();
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateTest(Test entity)
        {
            try
            {
                _dataRepositories.TestRepository.Edit(entity);
                _dataRepositories.TestRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Test> GetTestById(Guid id)
        {
            var data= await  _dataRepositories.TestRepository.GetById(id);
            data.TestType = await _dataRepositories.TestTypeRepository.GetById(data.TestTypeId);
            data.TestUnit = await _dataRepositories.TestUnitRepository.GetById(data.TestUnitId);
            return data;
        }
        public async Task<IEnumerable<Test>> GetAllTestByTestGroupId(Guid testgroupid)
        {
            var data = await _dataRepositories.TestRepository.FindBy(c => c.TestGroupId==testgroupid);
            return data.Select(c => new Test
            {
                TestId = c.TestId,
                TestName = c.TestName,
                TestGroupId = c.TestGroupId,
                TestDesciption = c.TestDesciption,
                HiLimit = c.HiLimit,
                LoLimit = c.LoLimit,
                TestTypeId = c.TestTypeId,
                TestUnitId = c.TestUnitId,
                Sequence = c.Sequence,
                TestType = Task.Run(async () => await _dataRepositories.TestTypeRepository.GetById(c.TestTypeId)).Result,
                TestUnit = Task.Run(async () => await _dataRepositories.TestUnitRepository.GetById(c.TestUnitId)).Result
            }).ToList();
        }
    }
}

