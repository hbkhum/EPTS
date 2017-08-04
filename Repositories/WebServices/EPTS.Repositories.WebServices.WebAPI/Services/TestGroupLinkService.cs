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
    public class TestGroupLinkService : ITestGroupLinkService
    {

        private readonly IDataRepositories _dataRepositories;

        public TestGroupLinkService(IDataRepositories dataRepositories)
        {
            _dataRepositories = dataRepositories;
        }

        public async Task<bool> CreateTestGroupLink(TestGroupLink entity)
        {
            try
            {
                var result = false;
                _dataRepositories.TestGroupLinkRepository.Add(entity);
                _dataRepositories.TestGroupLinkRepository.Save();
                if (entity.TestGroupLinkId != Guid.NewGuid()) result = true;
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

        public async Task<bool> DeleteTestGroupLink(Guid id)
        {
            try
            {
                var entity = await _dataRepositories.TestGroupLinkRepository.GetById(id);
                _dataRepositories.TestGroupLinkRepository.Delete(entity);
                _dataRepositories.TestGroupLinkRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<IEnumerable<TestGroupLink>> GetAllTestGroupLinks(string whereValue, string orderBy)
        {
            var result= await _dataRepositories.TestGroupLinkRepository.GetAllAsync(whereValue, orderBy);
            result = result.Select(c => new TestGroupLink
            {
                TestGroupLinkId = c.TestGroupLinkId,
                //TestGroupLinkName = c.TestGroupLinkName,
                //BusinessUnitId = c.BusinessUnitId,
                //BusinessUnit = Task.Run(async () => await _dataRepositories.BusinessUnitRepository.GetById(c.BusinessUnitId)).Result,
            }).ToList();
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateTestGroupLink(TestGroupLink entity)
        {
            try
            {
                _dataRepositories.TestGroupLinkRepository.Edit(entity);
                _dataRepositories.TestGroupLinkRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<TestGroupLink> GetTestGroupLinkById(Guid id)
        {
            return await  _dataRepositories.TestGroupLinkRepository.GetById(id);
        }
    }
}

