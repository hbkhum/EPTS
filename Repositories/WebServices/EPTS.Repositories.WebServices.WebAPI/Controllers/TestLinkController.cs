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
    [HubName("TestLinkHub")]
    public class TestLinkHub : Hub { }
    [RoutePrefix("api/TestLink")]
    public class TestLinkController : ApiControllerWithHub<TestLinkHub>
    {
        private readonly IDataServices _dataServices;

        public TestLinkController(IDataServices dataServices)
        {
            _dataServices = dataServices;

        }
        // GET: api/TestLinks
        // GET: api/TestLinks/pageSize?/pageNumber?/orderby(optional)/wherevalue/type
        [Route("{pageSize:int?}/{pageNumber:int?}/{orderby:alpha?}/{wherevalue:alpha?}/{type:alpha?}")]
        public async Task<IHttpActionResult> Get(int pageSize = 5000, int pageNumber = 1, string whereValue = "", string orderBy = "", string type = "json")
        {
            var result = await _dataServices.TestLinkService.GetAllTestLinks(whereValue, orderBy);
            var data = result as IList<TestLink> ?? result.ToList();
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

        // GET api/TestLink/5
        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> Get(Guid id, string type = "json")
        {
            var result = await _dataServices.TestLinkService.GetTestLinkById(id);
            if (type == "json")
            {
                return Ok(result);
            }
            return null;
        }
        [Route("")]
        public async Task<Guid?> Post(TestLink testlink)
        {
            var result = await _dataServices.TestLinkService.CreateTestLink(testlink);
            if (!result) return null;
            //SignalR Methods Add Element
            var context = GlobalHost.ConnectionManager.GetHubContext<TestLinkHub>();
            context.Clients.All.AddTestLink(testlink);
            var task = Task.Run(() => testlink.TestLinkId);
            return await task;
        }

        // PUT api/TestLink/5
        [Route("{id:Guid}")]
        //[HttpPut]
        public async Task<bool> Put(Guid id, TestLink testlink)
        {
            var result = await _dataServices.TestLinkService.UpdateTestLink(testlink);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Update
            var context = GlobalHost.ConnectionManager.GetHubContext<TestLinkHub>();
            context.Clients.All.UpdateTestLink(testlink);
            var task = Task.Run(() => true);
            return await task;

        }

        // DELETE api/TestLink/5
        [Route("{id:Guid}")]
        public async Task<bool> Delete(Guid id)
        {
            var testlink = await _dataServices.TestLinkService.GetTestLinkById(id);
            var result = await _dataServices.TestLinkService.DeleteTestLink(id);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Delete
            var context = GlobalHost.ConnectionManager.GetHubContext<TestLinkHub>();
            context.Clients.All.DeleteTestLink(testlink);
            var task = Task.Run(() => true);
            return await task;

        }
    }
}


