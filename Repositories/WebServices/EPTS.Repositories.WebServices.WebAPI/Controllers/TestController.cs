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
    [HubName("TestHub")]
    public class TestHub : Hub { }
    [RoutePrefix("api/Test")]
    public class TestController : ApiControllerWithHub<TestHub>
    {
        private readonly IDataServices _dataServices;

        public TestController(IDataServices dataServices)
        {
            _dataServices = dataServices;

        }
        // GET: api/Tests
        // GET: api/Tests/pageSize?/pageNumber?/orderby(optional)/wherevalue/type
        [Route("{pageSize:int?}/{pageNumber:int?}/{orderby:alpha?}/{wherevalue:alpha?}/{type:alpha?}")]
        public async Task<IHttpActionResult> Get(int pageSize = 5000, int pageNumber = 1, string whereValue = "", string orderBy = "", string type = "json")
        {
            var result = await _dataServices.TestService.GetAllTests(whereValue, orderBy);
            if (result == null) return null;
            var data = result as IList<Test> ?? result.ToList();
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

        // GET api/Test/5
        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> Get(Guid id, string type = "json")
        {
            var result = await _dataServices.TestService.GetTestById(id);
            if (type == "json")
            {
                return Ok(result);
            }
            return null;
        }

        [Route("TestGroup/{id:Guid}")]
        public async Task<IHttpActionResult> GetTestGroupByTestPlanId(Guid id, string type = "json")
        {
            var pageSize = 50000;
            var pageNumber = 1;
            var result = await _dataServices.TestService.GetAllTestByTestGroupId(id);
            var data = result as IList<Test> ?? result.ToList();
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
        [Route("")]
        public async Task<Guid?> Post(Test test)
        {
            var result = await _dataServices.TestService.CreateTest(test);
            if (!result) return null;
            //SignalR Methods Add Element
            var context = GlobalHost.ConnectionManager.GetHubContext<TestHub>();
            context.Clients.All.AddTest(test);
            var task = Task.Run(() => test.TestId);
            return await task;
        }

        // PUT api/Test/5
        [Route("{id:Guid}")]
        //[HttpPut]
        public async Task<bool> Put(Guid id, Test test)
        {
            var result = await _dataServices.TestService.UpdateTest(test);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Update
            var context = GlobalHost.ConnectionManager.GetHubContext<TestHub>();
            context.Clients.All.UpdateTest(test);
            var task = Task.Run(() => true);
            return await task;

        }

        // DELETE api/Test/5
        [Route("{id:Guid}")]
        public async Task<bool> Delete(Guid id)
        {
            var test = await _dataServices.TestService.GetTestById(id);
            var result = await _dataServices.TestService.DeleteTest(id);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Delete
            var context = GlobalHost.ConnectionManager.GetHubContext<TestHub>();
            context.Clients.All.DeleteTest(test);
            var task = Task.Run(() => true);
            return await task;

        }
    }
}


