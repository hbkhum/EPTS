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
    [HubName("TestPlanLinkHub")]
    public class TestPlanLinkHub : Hub { }
    [RoutePrefix("api/TestPlanLink")]
    public class TestPlanLinkController : ApiControllerWithHub<TestPlanLinkHub>
    {
        private readonly IDataServices _dataServices;

        public TestPlanLinkController(IDataServices dataServices)
        {
            _dataServices = dataServices;

        }
        // GET: api/TestPlanLinks
        // GET: api/TestPlanLinks/pageSize?/pageNumber?/orderby(optional)/wherevalue/type
        [Route("{pageSize:int?}/{pageNumber:int?}/{orderby:alpha?}/{wherevalue:alpha?}/{type:alpha?}")]
        public async Task<IHttpActionResult> Get(int pageSize = 5000, int pageNumber = 1, string whereValue = "", string orderBy = "", string type = "json")
        {
            var result = await _dataServices.TestPlanLinkService.GetAllTestPlanLinks(whereValue, orderBy);
            var data = result as IList<TestPlanLink> ?? result.ToList();
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

        // GET api/TestPlanLink/5
        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> Get(Guid id, string type = "json")
        {
            var result = await _dataServices.TestPlanLinkService.GetTestPlanLinkById(id);
            if (type == "json")
            {
                return Ok(result);
            }
            return null;
        }
        [Route("")]
        public async Task<Guid?> Post(TestPlanLink testplanlink)
        {
            var result = await _dataServices.TestPlanLinkService.CreateTestPlanLink(testplanlink);
            if (!result) return null;
            //SignalR Methods Add Element
            var context = GlobalHost.ConnectionManager.GetHubContext<TestPlanLinkHub>();
            context.Clients.All.AddTestPlanLink(testplanlink);
            var task = Task.Run(() => testplanlink.TestPlanLinkId);
            return await task;
        }

        // PUT api/TestPlanLink/5
        [Route("{id:Guid}")]
        //[HttpPut]
        public async Task<bool> Put(Guid id, TestPlanLink testplanlink)
        {
            var result = await _dataServices.TestPlanLinkService.UpdateTestPlanLink(testplanlink);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Update
            var context = GlobalHost.ConnectionManager.GetHubContext<TestPlanLinkHub>();
            context.Clients.All.UpdateTestPlanLink(testplanlink);
            var task = Task.Run(() => true);
            return await task;

        }

        // DELETE api/TestPlanLink/5
        [Route("{id:Guid}")]
        public async Task<bool> Delete(Guid id)
        {
            var testplanlink = await _dataServices.TestPlanLinkService.GetTestPlanLinkById(id);
            var result = await _dataServices.TestPlanLinkService.DeleteTestPlanLink(id);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Delete
            var context = GlobalHost.ConnectionManager.GetHubContext<TestPlanLinkHub>();
            context.Clients.All.DeleteTestPlanLink(testplanlink);
            var task = Task.Run(() => true);
            return await task;

        }

    }
}


