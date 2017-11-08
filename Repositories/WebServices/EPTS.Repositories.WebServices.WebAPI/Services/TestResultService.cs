using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;
using EPTS.Repositories.WebServices.WebAPI.Repositories;
using EPTS.Repositories.WebServices.WebAPI.Services.Interfaces;

namespace EPTS.Repositories.WebServices.WebAPI.Services
{
    public class TestResultService : ITestResultService
    {

        private readonly IDataRepositories _dataRepositories;

        public TestResultService(IDataRepositories dataRepositories)
        {
            _dataRepositories = dataRepositories;
        }

        public async Task<bool> CreateTestResult(TestResult entity)
        {
            try
            {
                var result = false;
                _dataRepositories.TestResultRepository.Add(entity);
                _dataRepositories.TestResultRepository.Save();
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

        public async Task<bool> DeleteTestResult(Guid id)
        {
            try
            {
                var entity = await _dataRepositories.TestResultRepository.GetById(id);
                _dataRepositories.TestResultRepository.Delete(entity);
                _dataRepositories.TestResultRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<IEnumerable<TestResult>> GetAllTestResults(string whereValue, string orderBy)
        {
            var result = await _dataRepositories.TestResultRepository.GetAllAsync(whereValue, orderBy);
            //result = result.Select(c => new Test
            //{
            //    TestId = c.TestId,
            //    TestName = c.TestName,
            //    //BusinessUnitId = c.BusinessUnitId,
            //    //BusinessUnit = Task.Run(async () => await _dataRepositories.BusinessUnitRepository.GetById(c.BusinessUnitId)).Result,
            //}).ToList();
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateTestResult(TestResult entity)
        {
            try
            {
                _dataRepositories.TestResultRepository.Edit(entity);
                _dataRepositories.TestResultRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<TestResult> GetTestResultById(Guid id)
        {
            return await _dataRepositories.TestResultRepository.GetById(id);
        }
        public async Task<TestPlan> GetAllTestByStationGroupCode(string code)
        {
            var stationgroup = await _dataRepositories.StationGroupRepository.GetByStationCode(code);
            stationgroup.TestPlan = await _dataRepositories.TestPlanRepository.GetById(stationgroup.TestPlanId);
            stationgroup.TestPlan.TestGroup =(ICollection<TestGroup>)await _dataRepositories.TestGroupRepository.FindBy(c => c.TestPlanId == stationgroup.TestPlanId);
            stationgroup.TestPlan.TestGroup = stationgroup.TestPlan.TestGroup.Select(x => new TestGroup
            {
                TestGroupId = x.TestGroupId,
                TestGroupName = x.TestGroupName,
                Description = x.Description,
                Sequence = x.Sequence,
                Test= Task.Run(async () =>
                {
                    var test = await _dataRepositories.TestRepository.FindBy(c => c.TestGroupId == x.TestGroupId);

                    return test.Select(c => new Test
                    {
                        TestId = c.TestId,
                        TestGroupId = c.TestGroupId,
                        TestTypeId = c.TestTypeId,
                        TestUnitId = c.TestUnitId,
                        TestDesciption = c.TestDesciption,
                        TestName = c.TestName,
                        HiLimit = c.HiLimit,
                        LoLimit = c.LoLimit,
                        Sequence = c.Sequence,
                        //TestGroup = c.TestGroup,
                        TestType = Task.Run(async () => await _dataRepositories.TestTypeRepository.GetById(c.TestTypeId)).Result,
                        TestUnit = Task.Run(async () => await _dataRepositories.TestUnitRepository.GetById(c.TestUnitId)).Result,
                    }).OrderBy( b => b.Sequence).ToList();
                }).Result,

            }).OrderBy( b => b.Sequence).ToList();
            return stationgroup.TestPlan;
            //var testgroup = await _dataRepositories.TestGroupRepository.FindBy(c => c.TestPlanId == testplan.TestPlanId);
            //var testgroupid = testgroup.Select(c => c.TestGroupId);
            //var test = await _dataRepositories.TestRepository.FindBy(c => testgroupid.Contains(c.TestGroupId));

            //test = test.Select(c => new Test
            //{
            //    TestId = c.TestId,
            //    TestGroupId = c.TestGroupId,
            //    TestTypeId = c.TestTypeId,
            //    TestUnitId = c.TestUnitId,
            //    TestDesciption = c.TestDesciption,
            //    TestName = c.TestName,
            //    HiLimit = c.HiLimit,
            //    LoLimit = c.LoLimit,
            //    Sequence = c.Sequence,
            //    TestGroup = c.TestGroup,
            //    TestType = Task.Run(async () => await _dataRepositories.TestTypeRepository.GetById(c.TestTypeId)).Result,
            //    TestUnit = Task.Run(async () => await _dataRepositories.TestUnitRepository.GetById(c.TestUnitId)).Result,
            //});

            //return test.Select(c => new TestResult
            //{
            //    TestId = c.TestId,
            //    Test = c,
            //    TestTypeId = c.TestTypeId,
            //    TestUnitId = c.TestUnitId,
            //    TestName = c.TestName,
            //    HiLimit = c.HiLimit,
            //    LoLimit = c.LoLimit,
            //    Sequence = c.Sequence,
            //    TestType = c.TestType,
            //    TestUnit = c.TestUnit,
            //    TestTypeName = c.TestType.TestTypeName,
            //    TestUnitName = c.TestUnit.TestUnitName,
            //    StarTime = null,
            //    FinishTime = null

            //});
            //testgroup.ToList().Contains()

            //var test = await _dataRepositories
            //    .TestRepository.FindBy(c => testgroup);


            //var data = await _dataRepositories.TestResultRepository.FindBy(c => c.TestGroupId == testgroupid);
            //return data.Select(c => new Test
            //{
            //    TestId = c.TestId,
            //    TestName = c.TestName,
            //    TestDesciption = c.TestDesciption,
            //    HiLimit = c.HiLimit,
            //    LoLimit = c.LoLimit,
            //    TestTypeId = c.TestTypeId,
            //    TestUnitId = c.TestUnitId,
            //    Sequence = c.Sequence,
            //    TestType = Task.Run(async () => await _dataRepositories.TestTypeRepository.GetById(c.TestTypeId)).Result,
            //    TestUnit = Task.Run(async () => await _dataRepositories.TestUnitRepository.GetById(c.TestUnitId)).Result
            //}).ToList();
            //return null;

        }
    }
}