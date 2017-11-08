using System;
using System.Data.Entity;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Core;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Interfaces;

namespace EPTS.Repositories.WebServices.WebAPI.Repositories
{
    public class StationGroupRepository : GenericRepositoryEntities<StationGroup>, IStationGroupsRepository
    {
        private DbContext _context;

        public StationGroupRepository(DbContext entities)
            : base(entities)
        {
            _context = entities;
        }

        public async Task<StationGroup> GetById(Guid id)
        {
            return await Dbset.FirstOrDefaultAsync(c => c.StationGroupId == id);
        }
        public async Task<StationGroup> GetByStationId(Guid id)
        {
            return await Dbset.FirstOrDefaultAsync(c => c.StationId == id);
        }
        public async Task<StationGroup> GetByStationCode(string code)
        {
            return await Dbset.FirstOrDefaultAsync(c => c.StationCode == code);
        }
    }
}