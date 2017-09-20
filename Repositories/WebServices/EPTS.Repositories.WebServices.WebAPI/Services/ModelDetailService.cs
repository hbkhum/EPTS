using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Repositories;
using EPTS.Repositories.WebServices.WebAPI.Services.Interfaces;

namespace EPTS.Repositories.WebServices.WebAPI.Services
{
    public class ModelDetailService : IModelDetailService
    {

        private readonly IDataRepositories _dataRepositories;

        public ModelDetailService(IDataRepositories dataRepositories)
        {
            _dataRepositories = dataRepositories;
        }

        public async Task<bool> CreateModelDetail(ModelDetail entity)
        {
            try
            {
                var result = false;
                _dataRepositories.ModelDetailRepository.Add(entity);
                _dataRepositories.ModelDetailRepository.Save();
                if (entity.ModelDetailId != Guid.NewGuid()) result = true;
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteModelDetail(Guid id)
        {
            try
            {
                var entity = await _dataRepositories.ModelDetailRepository.GetById(id);
                _dataRepositories.ModelDetailRepository.Delete(entity);
                _dataRepositories.ModelDetailRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<IEnumerable<ModelDetail>> GetAllModelDetails(string whereValue, string orderBy)
        {
            var result = await _dataRepositories.ModelDetailRepository.GetAllAsync(whereValue, orderBy);
            result = result.Select(c => new ModelDetail
            {
                ModelDetailId = c.ModelDetailId,
                ModelId = c.ModelId,
                PartNumberId = c.PartNumberId,
                //PartNumber = (ICollection<PartNumber>) Task.Run(async () => await _dataRepositories.PartNumberRepository.FindBy(n => n.PartNumberId== c.PartNumberId)).Result
            }).ToList();
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateModelDetail(ModelDetail entity)
        {
            try
            {
                _dataRepositories.ModelDetailRepository.Edit(entity);
                _dataRepositories.ModelDetailRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ModelDetail> GetModelDetailById(Guid id)
        {
            return await _dataRepositories.ModelDetailRepository.GetById(id);
        }
        public async Task<ModelDetail> GetModelDetailByPartNumberId(Guid id)
        {
            return await _dataRepositories.ModelDetailRepository.PartNumberGetById(id);
        }
    }
}
