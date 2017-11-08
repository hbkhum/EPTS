using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Interfaces;
using EPTS.Repositories.WebServices.WebAPI.Repositories;
using EPTS.Repositories.WebServices.WebAPI.Services.Interfaces;

namespace EPTS.Repositories.WebServices.WebAPI.Services
{
    public class ModelService : IModelService
    {

        private readonly IDataRepositories _dataRepositories;

        public ModelService(IDataRepositories dataRepositories)
        {
            _dataRepositories = dataRepositories;
        }

        public async Task<bool> CreateModel(Model entity)
        {
            try
            {
                var result = false;
                _dataRepositories.ModelRepository.Add(entity);
                _dataRepositories.ModelRepository.Save();
                if (entity.ModelId != Guid.NewGuid()) result = true;
                entity.Family =Task.Run(async () => await _dataRepositories
                    .FamilyRepository.GetById(entity.FamilyId)).Result;
                entity.ModelDetails = Task.Run(async () => await _dataRepositories
                      .ModelDetailRepository.FindBy(c => c.ModelId==entity.ModelId)).Result.ToList();

                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<bool> DeleteModel(Guid id)
        {
            try
            {
                var entity = await _dataRepositories.ModelRepository.GetById(id);
                _dataRepositories.ModelRepository.Delete(entity);
                _dataRepositories.ModelRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<IEnumerable<Model>> GetAllModels(string whereValue, string orderBy)
        {
            var result = await _dataRepositories.ModelRepository.GetAllAsync(whereValue, orderBy);
            result = result.Select(c => new Model
            {
                ModelId = c.ModelId,
                ModelName = c.ModelName,
                FamilyId = c.FamilyId,
                //Task.Run(async () =>
                //{
                //    var data = await _dataRepositories.PartNumberRepository.GetById(c.PartNumberId);
                //    data.ModelDetail = null;
                //    return data;
                //}).Result,
                Family = Task.Run(async () =>
                {
                    var data =await _dataRepositories.FamilyRepository.GetById(c.FamilyId);
                    data.BusinessUnit = await _dataRepositories.BusinessUnitRepository.GetById(c.Family.BusinessUnitId);
                    return data;

                }).Result,
                ModelDetails = Task.Run(async () => await _dataRepositories
                      .ModelDetailRepository
                        .FindBy(x => x.ModelId == c.ModelId)).Result.ToList()
            }).ToList();
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateModel(Model entity)
        {
            try
            {
                _dataRepositories.ModelRepository.Edit(entity);
                _dataRepositories.ModelRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Model> GetModelById(Guid id)
        {
            return await _dataRepositories.ModelRepository.GetById(id);
        }
    }
}
