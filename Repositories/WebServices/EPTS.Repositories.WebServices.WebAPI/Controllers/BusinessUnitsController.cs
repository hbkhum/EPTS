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
    [HubName("BusinessUnitHub")]
    public class BusinessUnitHub : Hub { }
    [RoutePrefix("api/BusinessUnit")]
    public class BusinessUnitController : ApiControllerWithHub<BusinessUnitHub>
    {
        private readonly IDataServices _dataServices;
        
        public BusinessUnitController(IDataServices dataServices)
        {
            _dataServices = dataServices;

        }
        // GET: api/BusinessUnit
        // GET: api/BusinessUnit/pageSize?/pageNumber?/orderby(optional)/wherevalue/type
        [Route("{pageSize:int?}/{pageNumber:int?}/{orderby:alpha?}/{wherevalue:alpha?}/{type:alpha?}")]
        public async Task<IHttpActionResult> Get(int pageSize = 5000, int pageNumber = 1, string whereValue = "", string orderBy = "", string type = "json")
        {
            var result = await _dataServices.BusinessUnitService.GetAllBusinessUnits(whereValue, orderBy);
            var data = result as IList<BusinessUnit> ?? result.ToList();
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

        // GET api/BusinessUnit/5
        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> Get(Guid id, string type = "json")
        {
            var result = await _dataServices.BusinessUnitService.GetBusinessUnitById(id);
            if (type == "json")
            {
                return Ok(result);
            }
            return null;
        }
        [Route("")]
        public async Task<Guid?> Post(BusinessUnit businessunit)
        {
            var result = await _dataServices.BusinessUnitService.CreateBusinessUnit(businessunit);
            if (!result) return null;
            //SignalR Methods Add Element
            var context = GlobalHost.ConnectionManager.GetHubContext<BusinessUnitHub>();
            context.Clients.All.AddBusinessUnit(businessunit);
            var task = Task.Run(() => businessunit.BusinessUnitId);
            return await task;
        }

        // PUT api/BusinessUnit/5
        [Route("{id:Guid}")]
        //[HttpPut]
        public async Task<bool> Put(Guid id,BusinessUnit businessunit)
        {
            var result = await _dataServices.BusinessUnitService.UpdateBusinessUnit(businessunit);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Update
            var context = GlobalHost.ConnectionManager.GetHubContext<BusinessUnitHub>();
            context.Clients.All.UpdateBusinessUnit(businessunit);
            var task = Task.Run(() => true);
            return await task;

        }

        // DELETE api/BusinessUnit/5
        [Route("{id:Guid}")]
        public async Task<bool> Delete(Guid id)
        {
            var businessunit = await _dataServices.BusinessUnitService.GetBusinessUnitById(id);
            var result = await _dataServices.BusinessUnitService.DeleteBusinessUnit(id);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Delete
            var context = GlobalHost.ConnectionManager.GetHubContext<BusinessUnitHub>();
            context.Clients.All.DeleteBusinessUnit(businessunit);
            var task = Task.Run(() => true);
            return await task;

        }
    }
}
