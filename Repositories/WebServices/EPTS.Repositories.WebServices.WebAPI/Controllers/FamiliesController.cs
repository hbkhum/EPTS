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
    [HubName("FamilyHub")]
    public class FamilyHub : Hub { }
    [RoutePrefix("api/Family")]
    public class FamilyController : ApiControllerWithHub<FamilyHub>
    {
        private readonly IDataServices _dataServices;

        public FamilyController(IDataServices dataServices)
        {
            _dataServices = dataServices;

        }
        // GET: api/Families
        // GET: api/Families/pageSize?/pageNumber?/orderby(optional)/wherevalue/type
        [Route("{pageSize:int?}/{pageNumber:int?}/{orderby:alpha?}/{wherevalue:alpha?}/{type:alpha?}")]
        public async Task<IHttpActionResult> Get(int pageSize = 5000, int pageNumber = 1, string whereValue = "", string orderBy = "", string type = "json")
        {
            whereValue = whereValue ?? "";
            var result = await _dataServices.FamilyService.GetAllFamilies(whereValue, orderBy);
            var data = result as IList<Family> ?? result.ToList();
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

        // GET api/Family/5
        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> Get(Guid id, string type = "json")
        {
            var result = await _dataServices.FamilyService.GetFamilyById(id);
            if (type == "json")
            {
                return Ok(result);
            }
            return null;
        }
        [Route("")]
        public async Task<Guid?> Post(Family family)
        {
            var result = await _dataServices.FamilyService.CreateFamily(family);
            if (!result) return null;
            //SignalR Methods Add Element
            var context = GlobalHost.ConnectionManager.GetHubContext<FamilyHub>();
            context.Clients.All.AddFamily(family);
            var task = Task.Run(() => family.FamilyId);
            return await task;
        }

        // PUT api/Family/5
        [Route("{id:Guid}")]
        //[HttpPut]
        public async Task<bool> Put(Guid id, Family family)
        {
            var result = await _dataServices.FamilyService.UpdateFamily(family);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Update
            var context = GlobalHost.ConnectionManager.GetHubContext<FamilyHub>();
            context.Clients.All.UpdateFamily(family);
            var task = Task.Run(() => true);
            return await task;

        }

        // DELETE api/Family/5
        [Route("{id:Guid}")]
        public async Task<bool> Delete(Guid id)
        {
            var family = await _dataServices.FamilyService.GetFamilyById(id);
            var result = await _dataServices.FamilyService.DeleteFamily(id);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Delete
            var context = GlobalHost.ConnectionManager.GetHubContext<FamilyHub>();
            context.Clients.All.DeleteFamily(family);
            var task = Task.Run(() => true);
            return await task;

        }
    }
}

