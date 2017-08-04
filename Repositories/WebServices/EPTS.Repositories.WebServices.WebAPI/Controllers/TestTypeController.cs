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
    [HubName("TestTypeHub")]
    public class TestTypeHub : Hub { }
    [RoutePrefix("api/TestType")]
    public class TestTypeController : ApiControllerWithHub<TestTypeHub>
    {
        private readonly IDataServices _dataServices;

        public TestTypeController(IDataServices dataServices)
        {
            _dataServices = dataServices;

        }
        // GET: api/TestTypes
        // GET: api/TestTypes/pageSize?/pageNumber?/orderby(optional)/wherevalue/type
        [Route("{pageSize:int?}/{pageNumber:int?}/{orderby:alpha?}/{wherevalue:alpha?}/{type:alpha?}")]
        public async Task<IHttpActionResult> Get(int pageSize = 5000, int pageNumber = 1, string whereValue = "", string orderBy = "", string type = "json")
        {
            var result = await _dataServices.TestTypeService.GetAllTestTypes(whereValue, orderBy);
            var data = result as IList<TestType> ?? result.ToList();
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

        // GET api/TestType/5
        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> Get(Guid id, string type = "json")
        {
            var result = await _dataServices.TestTypeService.GetTestTypeById(id);
            if (type == "json")
            {
                return Ok(result);
            }
            return null;
        }
        [Route("")]
        public async Task<Guid?> Post(TestType testtype)
        {
            var result = await _dataServices.TestTypeService.CreateTestType(testtype);
            if (!result) return null;
            //SignalR Methods Add Element
            var context = GlobalHost.ConnectionManager.GetHubContext<TestTypeHub>();
            context.Clients.All.AddTestType(testtype);
            var task = Task.Run(() => testtype.TestTypeId);
            return await task;
        }

        // PUT api/TestType/5
        [Route("{id:Guid}")]
        //[HttpPut]
        public async Task<bool> Put(Guid id, TestType testtype)
        {
            var result = await _dataServices.TestTypeService.UpdateTestType(testtype);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Update
            var context = GlobalHost.ConnectionManager.GetHubContext<TestTypeHub>();
            context.Clients.All.UpdateTestType(testtype);
            var task = Task.Run(() => true);
            return await task;

        }

        // DELETE api/TestType/5
        [Route("{id:Guid}")]
        public async Task<bool> Delete(Guid id)
        {
            var testtype = await _dataServices.TestTypeService.GetTestTypeById(id);
            var result = await _dataServices.TestTypeService.DeleteTestType(id);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Delete
            var context = GlobalHost.ConnectionManager.GetHubContext<TestTypeHub>();
            context.Clients.All.DeleteTestType(testtype);
            var task = Task.Run(() => true);
            return await task;

        }
    }
}


