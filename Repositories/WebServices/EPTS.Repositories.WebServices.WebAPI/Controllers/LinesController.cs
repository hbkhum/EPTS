using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Web.Http;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace EPTS.Repositories.WebServices.WebAPI.Controllers
{
    [HubName("LineHub")]
    public class LineHub : Hub { }
    [RoutePrefix("api/Lines")]
    public class LinesController : ApiControllerWithHub<LineHub>
    {
        private readonly IDataServices _dataServices;

        public LinesController(IDataServices dataServices)
        {
            _dataServices = dataServices;

        }

        // GET api/lineapi
        [Route("{pageSize:int?}/{pageNumber:int?}/{orderby:alpha?}/{wherevalue:alpha?}/{type:alpha?}")]

        public async Task<IHttpActionResult> Get(int pageSize = 5000, int pageNumber = 1, string whereValue = "", string orderBy = "", string type = "json")
        {
            var result = await _dataServices.LineService.GetAllLines(whereValue, orderBy);
            var data = result as IList<Line> ?? result.ToList();
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

        // GET api/lineapi/5
        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> Get(Guid id, string type = "json")
        {
            var result = await _dataServices.LineService.GetLineById(id);
            if (type == "json")
            {
                return Ok(result);
            }
            return null;
        }

        // POST api/lineapi
        [Route("")]
        public async Task<Guid?> Post(Line line)
        {
            var result = await _dataServices.LineService.CreateLine(line);
            if (!result) return null;
            //SignalR Methods Add Element
            var context = GlobalHost.ConnectionManager.GetHubContext<LineHub>();
            context.Clients.All.AddLine(line);
            var task = Task.Run(() => line.LineId);
            return await task;
        }

        // PUT api/lineapi/5
        [Route("{id:Guid}")]
        public async Task<bool> Put (Guid id, Line line)
        {
            var result = await _dataServices.LineService.UpdateLine(line);
            if (!result) return await Task.Run(() => false); ;
            //SignalR Methods Update
            var context = GlobalHost.ConnectionManager.GetHubContext<LineHub>();
            context.Clients.All.UpdateLine(line);
            var task = Task.Run(() => true);
            return await task;

        }

        // DELETE api/lineapi/5
        [Route("{id:Guid}")]
        public async Task<bool> Delete(Guid id)
        {
            var line = await _dataServices.LineService.GetLineById(id);
            var result = await _dataServices.LineService.DeleteLine(id);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Delete
            var context = GlobalHost.ConnectionManager.GetHubContext<LineHub>();
            context.Clients.All.DeleteLine(line);
            var task = Task.Run(() => true);
            return await task;

        }
    }
}
