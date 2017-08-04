using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Catalogs;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;
using EPTS.Repositories.WebServices.WebAPI.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace EPTS.Repositories.WebServices.WebAPI.Controllers
{
    [HubName("TestGroupLinkHub")]
    public class TestGroupLinkHub : Hub { }
    [RoutePrefix("api/TestGroupLink")]
    public class TestGroupLinkController : ApiControllerWithHub<TestGroupLinkHub>
    {
        private readonly IDataServices _dataServices;

        public TestGroupLinkController(IDataServices dataServices)
        {
            _dataServices = dataServices;

        }
        // GET: api/TestGroupLinks
        // GET: api/TestGroupLinks/pageSize?/pageNumber?/orderby(optional)/wherevalue/type
        [Route("{pageSize:int?}/{pageNumber:int?}/{orderby:alpha?}/{wherevalue:alpha?}/{type:alpha?}")]
        public async Task<IHttpActionResult> Get(int pageSize = 5000, int pageNumber = 1, string whereValue = "", string orderBy = "", string type = "json")
        {
            var result = await _dataServices.TestGroupLinkService.GetAllTestGroupLinks(whereValue, orderBy);
            var data = result as IList<TestGroupLink> ?? result.ToList();
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

        // GET api/TestGroupLink/5
        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> Get(Guid id, string type = "json")
        {
            var result = await _dataServices.TestGroupLinkService.GetTestGroupLinkById(id);
            if (type == "json")
            {
                return Ok(result);
            }
            return null;
        }
        [Route("")]
        public async Task<Guid?> Post(TestGroupLink testgrouplink)
        {
            var result = await _dataServices.TestGroupLinkService.CreateTestGroupLink(testgrouplink);
            if (!result) return null;
            //SignalR Methods Add Element
            var context = GlobalHost.ConnectionManager.GetHubContext<TestGroupLinkHub>();
            context.Clients.All.AddTestGroupLink(testgrouplink);
            var task = Task.Run(() => testgrouplink.TestGroupLinkId);
            return await task;
        }

        // PUT api/TestGroupLink/5
        [Route("{id:Guid}")]
        //[HttpPut]
        public async Task<bool> Put(Guid id, TestGroupLink testgrouplink)
        {
            var result = await _dataServices.TestGroupLinkService.UpdateTestGroupLink(testgrouplink);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Update
            var context = GlobalHost.ConnectionManager.GetHubContext<TestGroupLinkHub>();
            context.Clients.All.UpdateTestGroupLink(testgrouplink);
            var task = Task.Run(() => true);
            return await task;

        }

        // DELETE api/TestGroupLink/5
        [Route("{id:Guid}")]
        public async Task<bool> Delete(Guid id)
        {
            var testgrouplink = await _dataServices.TestGroupLinkService.GetTestGroupLinkById(id);
            var result = await _dataServices.TestGroupLinkService.DeleteTestGroupLink(id);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Delete
            var context = GlobalHost.ConnectionManager.GetHubContext<TestGroupLinkHub>();
            context.Clients.All.DeleteTestGroupLink(testgrouplink);
            var task = Task.Run(() => true);
            return await task;

        }
    }
}


