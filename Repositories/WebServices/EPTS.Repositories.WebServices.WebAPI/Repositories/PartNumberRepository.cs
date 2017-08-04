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
    public class PartNumberRepository : GenericRepositoryEntities<PartNumber>, IPartNumberRepository
    {
        private DbContext _context;

        public PartNumberRepository(DbContext entities)
            : base(entities)
        {
            _context = entities;
        }

        public async Task<PartNumber> GetById(Guid id)
        {
            return await Dbset.FirstOrDefaultAsync(c => c.PartNumberId == id);
        }

    }
}