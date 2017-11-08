using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Entities.Testing;
using EPTS.Repositories.WebServices.WebAPI.Services;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace EPTS.Repositories.WebServices.WebAPI.Controllers
{
    [HubName("TestGroupHub")]
    public class TestGroupHub : Hub { }
    [RoutePrefix("api/TestGroup")]
    public class TestGroupController : ApiControllerWithHub<TestGroupHub>
    {
        private readonly IDataServices _dataServices;

        public TestGroupController(IDataServices dataServices)
        {
            _dataServices = dataServices;

        }
        // GET: api/TestGroups
        // GET: api/TestGroups/pageSize?/pageNumber?/orderby(optional)/wherevalue/type
        [Route("{pageSize:int?}/{pageNumber:int?}/{orderby:alpha?}/{wherevalue:alpha?}/{type:alpha?}")]
        public async Task<IHttpActionResult> Get(int pageSize = 5000, int pageNumber = 1, string whereValue = "", string orderBy = "", string type = "json")
        {
            var result = await _dataServices.TestGroupService.GetAllTestGroups(whereValue, orderBy);
            var data = result as IList<TestGroup> ?? result.ToList();
            //TestPlan.TestGroup[0].Test[0]
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

        // GET api/TestGroup/5
        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> Get(Guid id, string type = "json")
        {
            var result = await _dataServices.TestGroupService.GetTestGroupById(id);
            if (type == "json")
            {
                return Ok(result);
            }
            return null;
        }
        [Route("TestPlan/{id:Guid}")]
        public async Task<IHttpActionResult> GetTestGroupByTestPlanId(Guid id, string type = "json")
        {
            var pageSize = 50000;
            var pageNumber = 1;
            var result = await _dataServices.TestGroupService.GetAllTestGroupByTestPlanId(id);
            var data = result as IList<TestGroup> ?? result.ToList();
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
        public async Task<Guid?> Post(TestGroup testgroup)
        {
            var result = await _dataServices.TestGroupService.CreateTestGroup(testgroup);
            if (!result) return null;
            //SignalR Methods Add Element
            var context = GlobalHost.ConnectionManager.GetHubContext<TestGroupHub>();
            context.Clients.All.AddTestGroup(testgroup);
            var task = Task.Run(() => testgroup.TestGroupId);
            return await task;
        }

        // PUT api/TestGroup/5
        [Route("{id:Guid}")]
        //[HttpPut]
        public async Task<bool> Put(Guid id, TestGroup testgroup)
        {
            var result = await _dataServices.TestGroupService.UpdateTestGroup(testgroup);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Update
            var context = GlobalHost.ConnectionManager.GetHubContext<TestGroupHub>();
            context.Clients.All.UpdateTestGroup(testgroup);
            var task = Task.Run(() => true);
            return await task;

        }

        // DELETE api/TestGroup/5
        [Route("{id:Guid}")]
        public async Task<bool> Delete(Guid id)
        {
            var testgroup = await _dataServices.TestGroupService.GetTestGroupById(id);
            var result = await _dataServices.TestGroupService.DeleteTestGroup(id);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Delete
            var context = GlobalHost.ConnectionManager.GetHubContext<TestGroupHub>();
            context.Clients.All.DeleteTestGroup(testgroup);
            var task = Task.Run(() => true);
            return await task;

        }
    }
}


