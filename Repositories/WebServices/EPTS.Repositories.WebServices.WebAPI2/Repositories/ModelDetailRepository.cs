using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Interfaces;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Core;

namespace EPTS.Repositories.WebServices.WebAPI.Repositories
{
    public class ModelDetailRepository : GenericRepositoryEntities<ModelDetail>, IModelDetailRepository
    {
        private DbContext _context;

        public ModelDetailRepository(DbContext entities)
            : base(entities)
        {
            _context = entities;
        }

        public async Task<ModelDetail> GetById(Guid id)
        {
            return await Dbset.FirstOrDefaultAsync(c => c.ModelDetailId == id);
        }
        public async Task<ModelDetail> ModelGetById(Guid modelid)
        {
            return await Dbset.FirstOrDefaultAsync(c => c.ModelId == modelid);
        }
        public async Task<ModelDetail> PartNumberGetById(Guid partnumberid)
        {
            return await Dbset.FirstOrDefaultAsync(c => c.PartNumberId == partnumberid);
        }
    }
}