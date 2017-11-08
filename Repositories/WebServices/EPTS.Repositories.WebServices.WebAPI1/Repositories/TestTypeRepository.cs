using System;
using System.Data.Entity;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Core;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Interfaces;

namespace EPTS.Repositories.WebServices.WebAPI.Repositories
{
    public class TestTypeRepository : GenericRepositoryEntities<TestType>, ITestTypeRepository
    {
        private DbContext _context;

        public TestTypeRepository(DbContext entities)
            : base(entities)
        {
            _context = entities;
        }

        public async Task<TestType> GetById(Guid id)
        {
            return await Dbset.FirstOrDefaultAsync(c => c.TestTypeId == id);
        }
    }
}