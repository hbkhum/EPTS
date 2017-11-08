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
    public class TestPlanService : ITestPlanService
    {

        private readonly IDataRepositories _dataRepositories;

        public TestPlanService(IDataRepositories dataRepositories)
        {
            _dataRepositories = dataRepositories;
        }

        public async Task<bool> CreateTestPlan(TestPlan entity)
        {
            try
            {
                var result = false;
                _dataRepositories.TestPlanRepository.Add(entity);
                _dataRepositories.TestPlanRepository.Save();
                if (entity.TestPlanId != Guid.NewGuid()) result = true;
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

        public async Task<bool> DeleteTestPlan(Guid id)
        {
            try
            {
                var entity = await _dataRepositories.TestPlanRepository.GetById(id);
                _dataRepositories.TestPlanRepository.Delete(entity);
                _dataRepositories.TestPlanRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<IEnumerable<TestPlan>> GetAllTestPlans(string whereValue, string orderBy)
        {
            var result= await _dataRepositories.TestPlanRepository.GetAllAsync(whereValue, orderBy);
            result = result.Select(c => new TestPlan
            {
                TestPlanId = c.TestPlanId,
                TestPlanName = c.TestPlanName,
                //TestGroup = (ICollection<TestGroup>) Task.Run(async () =>
                //{
                //    var data=await _dataRepositories.TestGroupRepository.FindBy(x => x.TestPlanId == c.TestPlanId);

                //    return data.Select(x => new TestGroup
                //    {
                //        TestGroupId = x.TestGroupId,
                //        TestPlanId = x.TestPlanId,
                //        TestGroupName = x.TestGroupName,
                //        TestPlan = 
                //    });
                //}).Result

            }).ToList();
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateTestPlan(TestPlan entity)
        {
            try
            {
                _dataRepositories.TestPlanRepository.Edit(entity);
                _dataRepositories.TestPlanRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<TestPlan> GetTestPlanById(Guid id)
        {
            return await  _dataRepositories.TestPlanRepository.GetById(id);
        }
    }
}

