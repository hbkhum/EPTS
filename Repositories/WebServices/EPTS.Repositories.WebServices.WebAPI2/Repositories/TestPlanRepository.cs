using System;
using System.Data.Entity;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Core;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Interfaces;

namespace EPTS.Repositories.WebServices.WebAPI.Repositories
{
    public class TestPlanRepository : GenericRepositoryEntities<TestPlan>, ITestPlanRepository
    {
        private DbContext _context;

        public TestPlanRepository(DbContext entities)
            : base(entities)
        {
            _context = entities;
        }

        public async Task<TestPlan> GetById(Guid id)
        {
            return await Dbset.FirstOrDefaultAsync(c => c.TestPlanId == id);
        }
    }
}