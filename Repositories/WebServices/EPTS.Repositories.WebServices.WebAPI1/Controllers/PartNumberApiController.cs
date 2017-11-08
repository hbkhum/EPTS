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
    [HubName("PartNumberHub")]
    public class PartNumberHub : Hub { }
    [RoutePrefix("api/PartNumber")]
    public class PartNumberApiController : ApiControllerWithHub<PartNumberHub>
    {
        private readonly IDataServices _dataServices;
        
        public PartNumberApiController(IDataServices dataServices)
        {
            _dataServices = dataServices;

        }

        // GET api/partnumberapi
        [Route("{pageSize:int?}/{pageNumber:int?}/{orderby:alpha?}/{wherevalue:alpha?}/{type:alpha?}")]
        public async Task<IHttpActionResult> Get(int pageSize = 5000, int pageNumber = 1, string whereValue = "", string orderBy = "", string type = "json")
        {
            whereValue = whereValue ?? "";
            var result = await _dataServices.PartNumberService.GetAllPartNumbers(whereValue, orderBy);
            var data = result as IList<PartNumber> ?? result.ToList();
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

        // GET api/partnumberapi/5
        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> Get(Guid id, string type = "json")
        {
            var result = await _dataServices.PartNumberService.GetPartNumberById(id);
            if (type == "json")
            {
                return Ok(result);
            }
            return null;
        }
        // GET api/partnumberapi/model/5
        [Route("Model/{id:Guid}/{pageSize:int?}/{pageNumber:int?}/{orderby:alpha?}/{wherevalue:alpha?}/{type:alpha?}")]
        public async Task<IHttpActionResult> GetPartNumberByModelId(Guid id, int pageSize = 5000, int pageNumber = 1, string whereValue = "", string orderBy = "", string type = "json")
        {
            whereValue = whereValue ?? "";
            var result = await _dataServices.PartNumberService.GetAllPartNumbersByModelId(id, whereValue, orderBy);
            var data = result as IList<ModelDetail> ?? result.ToList();
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


        // POST api/partnumberapi
        [Route("")]
        public async Task<Guid?> Post(PartNumber partnumber)
        {
            var result = await _dataServices.PartNumberService.CreatePartNumber(partnumber);
            if (!result) return null;
            //SignalR Methods Add Element
            var context = GlobalHost.ConnectionManager.GetHubContext<PartNumberHub>();
            context.Clients.All.AddPartNumber(partnumber);
            var task = Task.Run(() => partnumber.PartNumberId);
            return await task;
        }

        // PUT api/partnumberapi/5
        [Route("{id:Guid}")]
        public async Task<bool> Put(Guid id, PartNumber partnumber)
        {
            var result = await _dataServices.PartNumberService.UpdatePartNumber(partnumber);
            if (!result) return await Task.Run(() => false);
            ;
            //SignalR Methods Update
            var context = GlobalHost.ConnectionManager.GetHubContext<PartNumberHub>();
            context.Clients.All.UpdatePartNumber(partnumber);
            var task = Task.Run(() => true);
            return await task;
        }

        // DELETE api/partnumberapi/5
        [Route("{id:Guid}")]
        public async Task<bool> Delete(Guid id)
        {
            var partnumber = await _dataServices.PartNumberService.GetPartNumberById(id);
            var result = await _dataServices.PartNumberService.DeletePartNumber(id);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Delete
            var context = GlobalHost.ConnectionManager.GetHubContext<PartNumberHub>();
            context.Clients.All.DeletePartNumber(partnumber);
            var task = Task.Run(() => true);
            return await task;

        }
    }
}
