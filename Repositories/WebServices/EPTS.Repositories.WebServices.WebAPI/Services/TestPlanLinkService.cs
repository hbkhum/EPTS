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
    public class TestPlanLinkService : ITestPlanLinkService
    {

        private readonly IDataRepositories _dataRepositories;

        public TestPlanLinkService(IDataRepositories dataRepositories)
        {
            _dataRepositories = dataRepositories;
        }

        public async Task<bool> CreateTestPlanLink(TestPlanLink entity)
        {
            try
            {
                var result = false;
                _dataRepositories.TestPlanLinkRepository.Add(entity);
                _dataRepositories.TestPlanLinkRepository.Save();
                if (entity.TestPlanLinkId != Guid.NewGuid()) result = true;
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

        public async Task<bool> DeleteTestPlanLink(Guid id)
        {
            try
            {
                var entity = await _dataRepositories.TestPlanLinkRepository.GetById(id);
                _dataRepositories.TestPlanLinkRepository.Delete(entity);
                _dataRepositories.TestPlanLinkRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<IEnumerable<TestPlanLink>> GetAllTestPlanLinks(string whereValue, string orderBy)
        {
            var result= await _dataRepositories.TestPlanLinkRepository.GetAllAsync(whereValue, orderBy);
            result = result.Select(c => new TestPlanLink
            {
                TestPlanLinkId = c.TestPlanLinkId,
                //TestPlanLinkName = c.TestPlanLinkName,
                //BusinessUnitId = c.BusinessUnitId,
                //BusinessUnit = Task.Run(async () => await _dataRepositories.BusinessUnitRepository.GetById(c.BusinessUnitId)).Result,
            }).ToList();
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateTestPlanLink(TestPlanLink entity)
        {
            try
            {
                _dataRepositories.TestPlanLinkRepository.Edit(entity);
                _dataRepositories.TestPlanLinkRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<TestPlanLink> GetTestPlanLinkById(Guid id)
        {
            return await  _dataRepositories.TestPlanLinkRepository.GetById(id);
        }
    }
}

