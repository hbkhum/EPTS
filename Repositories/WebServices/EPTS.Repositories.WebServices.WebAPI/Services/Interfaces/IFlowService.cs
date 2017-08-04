using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;

namespace EPTS.Repositories.WebServices.WebAPI.Services.Interfaces
{
    public interface IFlowService
    {
        Task<bool> CreateFlow(Flow entity);
        Task<bool> DeleteFlow(Guid id);
        Task<IEnumerable<Flow>> GetAllFlows(string whereValue, string orderBy);
        Task<bool> UpdateFlow(Flow entity);
        Task<Flow> GetFlowById(Guid id);

    }
}