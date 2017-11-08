using System;
using System.Data.Entity;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Core;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Interfaces;

namespace EPTS.Repositories.WebServices.WebAPI.Repositories
{
    public class TestUnitRepository : GenericRepositoryEntities<TestUnit>, ITestUnitRepository
    {
        private DbContext _context;

        public TestUnitRepository(DbContext entities)
            : base(entities)
        {
            _context = entities;
        }

        public async Task<TestUnit> GetById(Guid id)
        {
            return await Dbset.FirstOrDefaultAsync(c => c.TestUnitId == id);
        }
    }
}