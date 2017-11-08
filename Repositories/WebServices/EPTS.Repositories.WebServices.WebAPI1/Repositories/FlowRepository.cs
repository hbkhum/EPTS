using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Interfaces;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Core;

namespace EPTS.Repositories.WebServices.WebAPI.Repositories
{
    public class FlowRepository : GenericRepositoryEntities<Flow>, IFlowRepository
    {
        private DbContext _context;

        public FlowRepository(DbContext entities)
            : base(entities)
        {
            _context = entities;
        }

        public async Task<Flow> GetById(Guid id)
        {
            return await Dbset.FirstOrDefaultAsync(c => c.FlowId == id);
        }
    }
}