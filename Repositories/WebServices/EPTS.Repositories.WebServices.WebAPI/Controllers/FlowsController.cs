using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace EPTS.Repositories.WebServices.WebAPI.Controllers
{
    [HubName("FlowHub")]
    public class FlowHub : Hub { }
    [RoutePrefix("api/Flows")]
    public class FlowsController : ApiControllerWithHub<FlowHub>
    {
        private readonly IDataServices _dataServices;

        public FlowsController(IDataServices dataServices)
        {
            _dataServices = dataServices;

        }

        // GET api/flowapi
        [Route("{pageSize:int?}/{pageNumber:int?}/{orderby:alpha?}/{wherevalue:alpha?}/{type:alpha?}")]
        public async Task<IHttpActionResult> Get(int pageSize = 5000, int pageNumber = 1, string whereValue = "", string orderBy = "", string type = "json")
        {
            var result = await _dataServices.FlowService.GetAllFlows(whereValue, orderBy);
            var data = result as IList<Flow> ?? result.ToList();
            var totalCount = data.Count();
            var totalPages = Math.Ceiling((double)totalCount / pageSize);
            if (type == "json")
            {
                var results = new
                {
                    TotalCount = totalCount,
                    totalPages = Math.Ceiling((double)totalCount / pageSize),
                    result = data
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList()
                };
                return Ok(results);
            }
            return null;

        }

        // GET api/flowapi/5
        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> Get(Guid id, string type = "json")
        {
            var result = await _dataServices.FlowService.GetFlowById(id);
            if (type == "json")
            {
                return Ok(result);
            }
            return null;
        }
        [Route("")]
        // POST api/flowapi
        public async Task<Guid?> Post(Flow flow)
        {
            var result = await _dataServices.FlowService.CreateFlow(flow);
            if (!result) return null;
            //SignalR Methods Add Element
            var context = GlobalHost.ConnectionManager.GetHubContext<FlowHub>();
            context.Clients.All.CreateFlow(flow);
            var task = Task.Run(() => flow.FlowId);
            return await task;
        }

        // PUT api/flowapi/5
        [Route("{id:Guid}")]
        public async Task<bool> Put(Guid id, Flow flow)
        {
            var result = await _dataServices.FlowService.UpdateFlow(flow);
            if (!result) return await Task.Run(() => false); ;
            //SignalR Methods Update
            var context = GlobalHost.ConnectionManager.GetHubContext<FlowHub>();
            context.Clients.All.UpdateFlow(flow);
            var task = Task.Run(() => true);
            return await task;

        }

        // DELETE api/flowapi/5
        [Route("{id:Guid}")]
        public async Task<bool> Delete(Guid id)
        {
            var flow = await _dataServices.FlowService.GetFlowById(id);
            var result = await _dataServices.FlowService.DeleteFlow(id);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Delete
            var context = GlobalHost.ConnectionManager.GetHubContext<FlowHub>();
            context.Clients.All.DeleteFlow(flow);
            var task = Task.Run(() => true);
            return await task;

        }
    }
}
