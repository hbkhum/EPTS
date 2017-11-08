using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Repositories;
using EPTS.Repositories.WebServices.WebAPI.Services.Interfaces;

namespace EPTS.Repositories.WebServices.WebAPI.Services
{
    public class LineService : ILineService
    {
        private readonly IDataRepositories _dataRepositories;

        public LineService(IDataRepositories dataRepositorieses)
        {
            _dataRepositories = dataRepositorieses;
        }
        public async Task<bool> CreateLine(Line entity)
        {
            try
            {
                var result = false;
                _dataRepositories.LineRepository.Add(entity);
                _dataRepositories.LineRepository.Save();
                if (entity.LineId != Guid.NewGuid()) result = true;
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteLine(Guid id)
        {
            try
            {
                var entity = await _dataRepositories.LineRepository.GetById(id);
                _dataRepositories.LineRepository.Delete(entity);
                _dataRepositories.LineRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<Line>> GetAllLines(string whereValue, string orderBy)
        {
            return await _dataRepositories.LineRepository.GetAllAsync(whereValue, orderBy);
        }

        public Task<Line> GetLineById(Guid id)
        {
            return _dataRepositories.LineRepository.GetById(id);
        }

        public async Task<bool> UpdateLine(Line entity)
        {
            try
            {
                _dataRepositories.LineRepository.Edit(entity);
                _dataRepositories.LineRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}