using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Repositories;
using EPTS.Repositories.WebServices.WebAPI.Services.Interfaces;

namespace EPTS.Repositories.WebServices.WebAPI.Services
{
    public class FlowService : IFlowService
    {

        private readonly IDataRepositories _dataRepositories;

        public FlowService(IDataRepositories dataRepositories)
        {
            _dataRepositories = dataRepositories;
        }

        public async Task<bool> CreateFlow(Flow entity)
        {
            try
            {
                var result = false;
                _dataRepositories.FlowRepository.Add(entity);
                _dataRepositories.FlowRepository.Save();
                if (entity.FlowId != Guid.NewGuid()) result = true;
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteFlow(Guid id)
        {
            try
            {
                var entity = await _dataRepositories.FlowRepository.GetById(id);
                _dataRepositories.FlowRepository.Delete(entity);
                _dataRepositories.FlowRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<IEnumerable<Flow>> GetAllFlows(string whereValue, string orderBy)
        {
            return await _dataRepositories.FlowRepository.GetAllAsync(whereValue, orderBy);
        }

        public async Task<bool> UpdateFlow(Flow entity)
        {
            try
            {
                _dataRepositories.FlowRepository.Edit(entity);
                _dataRepositories.FlowRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Flow> GetFlowById(Guid id)
        {
            return await _dataRepositories.FlowRepository.GetById(id);

        }
    }
}
