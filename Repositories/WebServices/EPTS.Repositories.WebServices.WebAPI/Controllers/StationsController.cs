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
    [HubName("StationHub")]
    public class StationHub : Hub { }
    [RoutePrefix("api/Station")]
    public class StationsController : ApiControllerWithHub<StationHub>
    {
        private readonly IDataServices _dataServices;
        
        public StationsController(IDataServices dataServices)
        {
            _dataServices = dataServices;

        }

        // GET api/stationapi
        [Route("{pageSize:int?}/{pageNumber:int?}/{orderby:alpha?}/{wherevalue:alpha?}/{type:alpha?}")]
        public async Task<IHttpActionResult> Get(int pageSize = 5000, int pageNumber = 1, string whereValue = "", string orderBy = "", string type = "json")
        {
            var result = await _dataServices.StationService.GetAllStations(whereValue, orderBy);
            var data = result as IList<Station> ?? result.ToList();
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

        // GET api/stationapi/5
        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> Get(Guid id, string type = "json")
        {
            var result = await _dataServices.StationService.GetStationById(id);
            if (type == "json")
            {
                return Ok(result);
            }
            return null;
        }
        [Route("")]
        // POST api/stationapi
        public async Task<Guid?> Post( Station station)
        {
            var result = await _dataServices.StationService.CreateStation(station);
            if (!result) return null;
            //SignalR Methods Add Element
            var context = GlobalHost.ConnectionManager.GetHubContext<StationHub>();
            context.Clients.All.AddStation(station);
            var task = Task.Run(() => station.StationId);
            return await task;
        }

        // PUT api/stationapi/5
        [Route("{id:Guid}")]
        public async Task<bool> Put(Guid id, Station station)
        {
            var result = await _dataServices.StationService.UpdateStation(station);
            if (!result) return await Task.Run(() => false);
            ;
            //SignalR Methods Update
            var context = GlobalHost.ConnectionManager.GetHubContext<StationHub>();
            context.Clients.All.UpdateStation(station);
            var task = Task.Run(() => true);
            return await task;
        }

        // DELETE api/stationapi/5
        [Route("{id:Guid}")]
        public async Task<bool> Delete(Guid id)
        {
            var station = await _dataServices.StationService.GetStationById(id);
            var result = await _dataServices.StationService.DeleteStation(id);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Delete
            var context = GlobalHost.ConnectionManager.GetHubContext<StationHub>();
            context.Clients.All.DeleteStation(station);
            var task = Task.Run(() => true);
            return await task;

        }
    }
}
