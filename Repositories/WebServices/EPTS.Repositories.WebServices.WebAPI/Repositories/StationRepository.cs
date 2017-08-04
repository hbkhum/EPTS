using System;
using System.Data.Entity;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Core;

namespace EPTS.Repositories.WebServices.WebAPI.Repositories
{
    public class StationRepository : GenericRepositoryEntities<Station>, IStationRepository
    {
        private DbContext _context;

        public StationRepository(DbContext entities)
            : base(entities)
        {
            _context = entities;
        }

        public async Task<Station> GetById(Guid id)
        {
            return await  Dbset.FirstOrDefaultAsync(c => c.StationId == id);
        }
    }
}