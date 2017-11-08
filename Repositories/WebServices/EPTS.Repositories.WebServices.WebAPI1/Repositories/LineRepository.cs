using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Interfaces;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Core;


namespace EPTS.Repositories.WebServices.WebAPI.Repositories
{
    public class LineRepository : GenericRepositoryEntities<Line>, ILineRepository
    {
        private DbContext _context;

        public LineRepository(DbContext entities)
            : base(entities)
        {
            _context = entities;
        }

        public async Task<Line> GetById(Guid id)
        {
            return await Dbset.FirstOrDefaultAsync(c => c.LineId == id);
        }
    }
}