using System;
using System.Data.Entity;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Core;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Interfaces;

namespace EPTS.Repositories.WebServices.WebAPI.Repositories
{
    public class TestGroupRepository : GenericRepositoryEntities<TestGroup>, ITestGroupRepository
    {
        private DbContext _context;

        public TestGroupRepository(DbContext entities)
            : base(entities)
        {
            _context = entities;
        }

        public async Task<TestGroup> GetById(Guid id)
        {
            return await Dbset.FirstOrDefaultAsync(c => c.TestGroupId == id);
        }
        public async Task<TestGroup> GetByTestPlanId(Guid id)
        {
            return await Dbset.FirstOrDefaultAsync(c => c.TestPlanId == id);
        }
    }
}