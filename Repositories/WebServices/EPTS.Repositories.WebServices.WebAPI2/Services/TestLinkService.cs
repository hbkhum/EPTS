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
    public class TestLinkService : ITestLinkService
    {

        private readonly IDataRepositories _dataRepositories;

        public TestLinkService(IDataRepositories dataRepositories)
        {
            _dataRepositories = dataRepositories;
        }

        public async Task<bool> CreateTestLink(TestLink entity)
        {
            try
            {
                var result = false;
                _dataRepositories.TestLinkRepository.Add(entity);
                _dataRepositories.TestLinkRepository.Save();
                if (entity.TestLinkId != Guid.NewGuid()) result = true;
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

        public async Task<bool> DeleteTestLink(Guid id)
        {
            try
            {
                var entity = await _dataRepositories.TestLinkRepository.GetById(id);
                _dataRepositories.TestLinkRepository.Delete(entity);
                _dataRepositories.TestLinkRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<IEnumerable<TestLink>> GetAllTestLinks(string whereValue, string orderBy)
        {
            var result= await _dataRepositories.TestLinkRepository.GetAllAsync(whereValue, orderBy);
            result = result.Select(c => new TestLink
            {
                TestLinkId = c.TestLinkId,
                //TestLinkName = c.TestLinkName,
                //BusinessUnitId = c.BusinessUnitId,
                //BusinessUnit = Task.Run(async () => await _dataRepositories.BusinessUnitRepository.GetById(c.BusinessUnitId)).Result,
            }).ToList();
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateTestLink(TestLink entity)
        {
            try
            {
                _dataRepositories.TestLinkRepository.Edit(entity);
                _dataRepositories.TestLinkRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<TestLink> GetTestLinkById(Guid id)
        {
            return await  _dataRepositories.TestLinkRepository.GetById(id);
        }
    }
}

