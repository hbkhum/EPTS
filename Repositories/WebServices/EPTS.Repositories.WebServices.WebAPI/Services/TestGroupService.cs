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
    public class TestGroupService : ITestGroupService
    {

        private readonly IDataRepositories _dataRepositories;

        public TestGroupService(IDataRepositories dataRepositories)
        {
            _dataRepositories = dataRepositories;
        }

        public async Task<bool> CreateTestGroup(TestGroup entity)
        {
            try
            {
                var result = false;
                _dataRepositories.TestGroupRepository.Add(entity);
                _dataRepositories.TestGroupRepository.Save();
                if (entity.TestGroupId != Guid.NewGuid()) result = true;
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

        public async Task<bool> DeleteTestGroup(Guid id)
        {
            try
            {
                var entity = await _dataRepositories.TestGroupRepository.GetById(id);
                _dataRepositories.TestGroupRepository.Delete(entity);
                _dataRepositories.TestGroupRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<IEnumerable<TestGroup>> GetAllTestGroups(string whereValue, string orderBy)
        {
            return  await _dataRepositories.TestGroupRepository.GetAllAsync(whereValue, orderBy);
            //return await Task.FromResult(result);
        }

        public async Task<bool> UpdateTestGroup(TestGroup entity)
        {
            try
            {
                _dataRepositories.TestGroupRepository.Edit(entity);
                _dataRepositories.TestGroupRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<TestGroup> GetTestGroupById(Guid id)
        {
            return await  _dataRepositories.TestGroupRepository.GetById(id);
        }
        public async Task<IEnumerable<TestGroup>> GetAllTestGroupByTestPlanId(Guid testplanid)
        {
            return  await _dataRepositories.TestGroupRepository.FindBy(c => c.TestPlanId==testplanid);
            //var testgroup = testplanlinks.Select(c => c.TestGroupId);
            //var data = await _dataRepositories.TestGroupRepository.FindBy(c => testgroup.Contains(c.TestGroupId));
            //return data.Select(c => new TestGroup
            //{
            //    TestGroupId = c.TestGroupId,
            //    TestGroupName = c.TestGroupName,
            //    TestPlanLink = c.TestPlanLink,
            //    TestGroupLink = (ICollection<TestGroupLink>) Task.Run(async () => await _dataRepositories.TestGroupLinkRepository.FindBy(x => x.TestGroupId==c.TestGroupId)).Result
            //}).ToList();
        }
    }
}

