using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Repositories;
using EPTS.Repositories.WebServices.WebAPI.Services.Interfaces;

namespace EPTS.Repositories.WebServices.WebAPI.Services
{
    public class FamilyService : IFamilyService
    {

        private readonly IDataRepositories _dataRepositories;

        public FamilyService(IDataRepositories dataRepositories)
        {
            _dataRepositories = dataRepositories;
        }

        public async Task<bool> CreateFamily(Family entity)
        {
            try
            {
                var result = false;
                _dataRepositories.FamilyRepository.Add(entity);
                _dataRepositories.FamilyRepository.Save();
                if (entity.FamilyId != Guid.NewGuid()) result = true;
                entity.BusinessUnit =
                    Task.Run(async () => await _dataRepositories.BusinessUnitRepository.GetById(entity.BusinessUnitId))
                        .Result;
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public async Task<bool> DeleteFamily(Guid id)
        {
            try
            {
                var entity = await _dataRepositories.FamilyRepository.GetById(id);
                _dataRepositories.FamilyRepository.Delete(entity);
                _dataRepositories.FamilyRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<IEnumerable<Family>> GetAllFamilies(string whereValue, string orderBy)
        {
            var result= await _dataRepositories.FamilyRepository.GetAllAsync(whereValue, orderBy);
            result = result.Select(c => new Family
            {
                FamilyId = c.FamilyId,
                FamilyName = c.FamilyName,
                BusinessUnitId = c.BusinessUnitId,
                BusinessUnit = Task.Run(async () => await _dataRepositories.BusinessUnitRepository.GetById(c.BusinessUnitId)).Result,
            }).ToList();
            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateFamily(Family entity)
        {
            try
            {
                _dataRepositories.FamilyRepository.Edit(entity);
                _dataRepositories.FamilyRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Family> GetFamilyById(Guid id)
        {
            return await  _dataRepositories.FamilyRepository.GetById(id);
        }
    }
}
