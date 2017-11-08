using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Interfaces;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Core;

namespace EPTS.Repositories.WebServices.WebAPI.Repositories
{
    public class FamilyRepository : GenericRepositoryEntities<Family>, IFamilyRepository
    {
        private DbContext _context;

        public FamilyRepository(DbContext entities)
            : base(entities)
        {
            _context = entities;
        }

        public async Task<Family> GetById(Guid id)
        {
            return await Dbset.FirstOrDefaultAsync(c => c.FamilyId == id);
        }
    }
}