using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Interfaces;
using EPTS.Repositories.WebServices.WebAPI.Services.Interfaces;
using EPTS.Repositories.WebServices.WebAPI.Repositories;

namespace EPTS.Repositories.WebServices.WebAPI.Services
{
    public class PartNumberService : IPartNumberService
    {

        private readonly IDataRepositories _dataRepositories;

        public PartNumberService(IDataRepositories dataRepositories)
        {
            _dataRepositories = dataRepositories;
        }

        public async Task<bool> CreatePartNumber(PartNumber entity)
        {
            try
            {
                var result = false;
                _dataRepositories.PartNumberRepository.Add(entity);
                _dataRepositories.PartNumberRepository.Save();
                if (entity.PartNumberId != Guid.NewGuid()) result = true;
                return await Task.FromResult(result);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeletePartNumber(Guid id)
        {
            try
            {
                var entity = await _dataRepositories.PartNumberRepository.GetById(id);
                _dataRepositories.PartNumberRepository.Delete(entity);
                _dataRepositories.PartNumberRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public async Task<IEnumerable<PartNumber>> GetAllPartNumbers(string whereValue, string orderBy)
        {
            var result= await _dataRepositories.PartNumberRepository.GetAllAsync(whereValue, orderBy);
            //result = result.Select(c => new List<ModelDetail>
            //{
            //    new ModelDetail
            //    {
            //        ModelId  = c.
            //    }
            //}).ToList();
            return await Task.FromResult(result);
        }
        public async Task<IEnumerable<ModelDetail>> GetAllPartNumbersByModelId(Guid modelid, string whereValue, string orderBy)
        {
            var result = await _dataRepositories.ModelDetailRepository.GetAllAsync("ModelId=\"" + modelid+ "\" and " + whereValue , orderBy);
            //var modeldetails = result as IList<ModelDetail> ?? result.ToList();
            //var partnumber = modeldetails.Select(c => c.PartNumberId );
            
            //var data= await _dataRepositories.PartNumberRepository.FindBy(c => partnumber.Contains(c.PartNumberId));
            return result.Select(c => new ModelDetail
            {
                ModelDetailId = c.ModelDetailId,
                PartNumberId = c.PartNumberId,
                ModelId = modelid,
                PartNumber = Task.Run(async () => await _dataRepositories.PartNumberRepository.GetById(c.PartNumberId)).Result,
                //Model = c
                //ModelDetailId = modeldetails[0].ModelDetailId,
                //PartNumberName = c.PartNumberName,
                //Description = c.Description,
                //Revision = c.Revision,
                //Active = c.Active,
                //ModelDetail= Task.Run(async () => await _dataRepositories.ModelDetailRepository.GetAllAsync("ModelId=\"" + modelid + "\"", "")).Result
                //ModelDetail = (ICollection<ModelDetail>) modeldetails,
            }).ToList();

        }

        public async Task<bool> UpdatePartNumber(PartNumber entity)
        {
            try
            {
                _dataRepositories.PartNumberRepository.Edit(entity);
                _dataRepositories.PartNumberRepository.Save();
                return await Task.FromResult(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<PartNumber> GetPartNumberById(Guid id)
        {
            return await _dataRepositories.PartNumberRepository.GetById(id);
        }

    }
}
