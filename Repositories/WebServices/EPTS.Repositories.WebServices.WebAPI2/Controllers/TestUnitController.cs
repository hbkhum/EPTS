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
    [HubName("TestUnitHub")]
    public class TestUnitHub : Hub { }
    [RoutePrefix("api/TestUnit")]
    public class TestUnitController : ApiControllerWithHub<TestUnitHub>
    {
        private readonly IDataServices _dataServices;

        public TestUnitController(IDataServices dataServices)
        {
            _dataServices = dataServices;

        }
        // GET: api/TestUnits
        // GET: api/TestUnits/pageSize?/pageNumber?/orderby(optional)/wherevalue/type
        [Route("{pageSize:int?}/{pageNumber:int?}/{orderby:alpha?}/{wherevalue:alpha?}/{type:alpha?}")]
        public async Task<IHttpActionResult> Get(int pageSize = 5000, int pageNumber = 1, string whereValue = "", string orderBy = "", string type = "json")
        {
            var result = await _dataServices.TestUnitService.GetAllTestUnits(whereValue, orderBy);
            var data = result as IList<TestUnit> ?? result.ToList();
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

        // GET api/TestUnit/5
        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> Get(Guid id, string type = "json")
        {
            var result = await _dataServices.TestUnitService.GetTestUnitById(id);
            if (type == "json")
            {
                return Ok(result);
            }
            return null;
        }
        [Route("")]
        public async Task<Guid?> Post(TestUnit testunit)
        {
            var result = await _dataServices.TestUnitService.CreateTestUnit(testunit);
            if (!result) return null;
            //SignalR Methods Add Element
            var context = GlobalHost.ConnectionManager.GetHubContext<TestUnitHub>();
            context.Clients.All.AddTestUnit(testunit);
            var task = Task.Run(() => testunit.TestUnitId);
            return await task;
        }

        // PUT api/TestUnit/5
        [Route("{id:Guid}")]
        //[HttpPut]
        public async Task<bool> Put(Guid id, TestUnit testunit)
        {
            var result = await _dataServices.TestUnitService.UpdateTestUnit(testunit);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Update
            var context = GlobalHost.ConnectionManager.GetHubContext<TestUnitHub>();
            context.Clients.All.UpdateTestUnit(testunit);
            var task = Task.Run(() => true);
            return await task;

        }

        // DELETE api/TestUnit/5
        [Route("{id:Guid}")]
        public async Task<bool> Delete(Guid id)
        {
            var testunit = await _dataServices.TestUnitService.GetTestUnitById(id);
            var result = await _dataServices.TestUnitService.DeleteTestUnit(id);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Delete
            var context = GlobalHost.ConnectionManager.GetHubContext<TestUnitHub>();
            context.Clients.All.DeleteTestUnit(testunit);
            var task = Task.Run(() => true);
            return await task;

        }
    }
}


