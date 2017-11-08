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
    [HubName("TestResultHub")]
    public class TestResultHub : Hub { }
    [RoutePrefix("api/TestResult")]
    public class TestResultController : ApiControllerWithHub<TestResultHub>
    {
        private readonly IDataServices _dataServices;

        public TestResultController(IDataServices dataServices)
        {
            _dataServices = dataServices;

        }
        // GET: api/Tests
        // GET: api/Tests/pageSize?/pageNumber?/orderby(optional)/wherevalue/type
        [Route("{pageSize:int?}/{pageNumber:int?}/{orderby:alpha?}/{wherevalue:alpha?}/{type:alpha?}")]
        public async Task<IHttpActionResult> Get(int pageSize = 5000, int pageNumber = 1, string whereValue = "", string orderBy = "", string type = "json")
        {
            var result = await _dataServices.TestResultService.GetAllTestResults(whereValue, orderBy);
            if (result == null) return null;
            var data = result as IList<TestResult> ?? result.ToList();
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

        // GET api/TestResult/5
        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> Get(Guid id, string type = "json")
        {
            var result = await _dataServices.TestResultService.GetTestResultById(id);
            if (type == "json")
            {
                return Ok(result);
            }
            return null;
        }


        [Route("")]
        public async Task<Guid?> Post(TestResult testresult)
        {
            var result = await _dataServices.TestResultService.CreateTestResult(testresult);
            if (!result) return null;
            //SignalR Methods Add Element
            var context = GlobalHost.ConnectionManager.GetHubContext<TestResultHub>();
            context.Clients.All.AddTest(testresult);
            var task = Task.Run(() => testresult.TestId);
            return await task;
        }

        // PUT api/TestResult/5
        [Route("{id:Guid}")]
        //[HttpPut]
        public async Task<bool> Put(Guid id, TestResult testresult)
        {
            var result = await _dataServices.TestResultService.UpdateTestResult(testresult);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Update
            var context = GlobalHost.ConnectionManager.GetHubContext<TestResultHub>();
            context.Clients.All.UpdateTest(testresult);
            var task = Task.Run(() => true);
            return await task;

        }

        // DELETE api/TestResult/5
        [Route("{id:Guid}")]
        public async Task<bool> Delete(Guid id)
        {
            var testresult = await _dataServices.TestResultService.GetTestResultById(id);
            var result = await _dataServices.TestResultService.DeleteTestResult(id);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Delete
            var context = GlobalHost.ConnectionManager.GetHubContext<TestResultHub>();
            context.Clients.All.DeleteTest(testresult);
            var task = Task.Run(() => true);
            return await task;

        }


    }
}


