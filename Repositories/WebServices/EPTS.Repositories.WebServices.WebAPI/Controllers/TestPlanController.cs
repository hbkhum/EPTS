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
    [HubName("TestPlanHub")]
    public class TestPlanHub : Hub { }
    [RoutePrefix("api/TestPlan")]
    public class TestPlanController : ApiControllerWithHub<TestPlanHub>
    {
        private readonly IDataServices _dataServices;

        public TestPlanController(IDataServices dataServices)
        {
            _dataServices = dataServices;

        }
        // GET: api/TestPlans
        // GET: api/TestPlans/pageSize?/pageNumber?/orderby(optional)/wherevalue/type
        [Route("{pageSize:int?}/{pageNumber:int?}/{orderby:alpha?}/{wherevalue:alpha?}/{type:alpha?}")]
        public async Task<IHttpActionResult> Get(int pageSize = 5000, int pageNumber = 1, string whereValue = "", string orderBy = "", string type = "json")
        {
            var result = await _dataServices.TestPlanService.GetAllTestPlans(whereValue, orderBy);
            var data = result as IList<TestPlan> ?? result.ToList();
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

        // GET api/TestPlan/5
        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> Get(Guid id, string type = "json")
        {
            var result = await _dataServices.TestPlanService.GetTestPlanById(id);
            if (type == "json")
            {
                return Ok(result);
            }
            return null;
        }
        [Route("")]
        public async Task<Guid?> Post(TestPlan testplan)
        {
            var result = await _dataServices.TestPlanService.CreateTestPlan(testplan);
            if (!result) return null;
            //SignalR Methods Add Element
            var context = GlobalHost.ConnectionManager.GetHubContext<TestPlanHub>();
            context.Clients.All.AddTestPlan(testplan);
            var task = Task.Run(() => testplan.TestPlanId);
            return await task;
        }

        // PUT api/TestPlan/5
        [Route("{id:Guid}")]
        //[HttpPut]
        public async Task<bool> Put(Guid id, TestPlan testplan)
        {
            var result = await _dataServices.TestPlanService.UpdateTestPlan(testplan);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Update
            var context = GlobalHost.ConnectionManager.GetHubContext<TestPlanHub>();
            context.Clients.All.UpdateTestPlan(testplan);
            var task = Task.Run(() => true);
            return await task;

        }

        // DELETE api/TestPlan/5
        [Route("{id:Guid}")]
        public async Task<bool> Delete(Guid id)
        {
            var testplan = await _dataServices.TestPlanService.GetTestPlanById(id);
            var result = await _dataServices.TestPlanService.DeleteTestPlan(id);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Delete
            var context = GlobalHost.ConnectionManager.GetHubContext<TestPlanHub>();
            context.Clients.All.DeleteTestPlan(testplan);
            var task = Task.Run(() => true);
            return await task;

        }
    }
}


