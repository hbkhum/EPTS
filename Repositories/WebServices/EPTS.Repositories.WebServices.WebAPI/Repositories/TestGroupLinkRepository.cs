using System;
using System.Data.Entity;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Core;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Interfaces;

namespace EPTS.Repositories.WebServices.WebAPI.Repositories
{
    public class TestGroupLinkRepository : GenericRepositoryEntities<TestGroupLink>, ITestGroupLinkRepository
    {
        private DbContext _context;

        public TestGroupLinkRepository(DbContext entities)
            : base(entities)
        {
            _context = entities;
        }

        public async Task<TestGroupLink> GetById(Guid id)
        {
            return await Dbset.FirstOrDefaultAsync(c => c.TestGroupLinkId == id);
        }
    }
}