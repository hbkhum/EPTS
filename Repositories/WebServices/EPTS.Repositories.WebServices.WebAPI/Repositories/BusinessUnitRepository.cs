using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Core;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Interfaces;

namespace EPTS.Repositories.WebServices.WebAPI.Repositories
{
    public class BusinessUnitRepository : GenericRepositoryEntities<BusinessUnit>, IBusinessUnitRepository
    {
        private DbContext _context;

        public BusinessUnitRepository(DbContext entities)
            : base(entities)
        {
            _context = entities;
        }

        public async Task<BusinessUnit> GetById(Guid id)
        {
            var task = Task.Run(() => Dbset.FirstOrDefault(c => c.BusinessUnitId == id));
            return await task;

        }
    }
}