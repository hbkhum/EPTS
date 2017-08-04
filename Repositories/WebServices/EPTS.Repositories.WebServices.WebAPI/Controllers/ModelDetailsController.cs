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
    [HubName("ModelDetailHub")]
    public class ModelDetailHub : Hub { }
    [RoutePrefix("api/ModelDetail")]
    public class ModelDetailsController : ApiControllerWithHub<ModelDetailHub>
    {
        private readonly IDataServices _dataServices;

        public ModelDetailsController(IDataServices dataServices)
        {
            _dataServices = dataServices;
        }

        // GET api/modeldetailapi
        [Route("{pageSize:int?}/{pageNumber:int?}/{orderby:alpha?}/{wherevalue:alpha?}/{type:alpha?}")]
        public async Task<IHttpActionResult> Get(int pageSize = 5000, int pageNumber = 1, string whereValue = "", string orderBy = "", string type = "json")
        {
            var result = await _dataServices.ModelDetailService.GetAllModelDetails(whereValue, orderBy);
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

        // GET api/modeldetailapi/5
        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> Get(Guid id, string type = "json")
        {
            var result = await _dataServices.ModelDetailService.GetModelDetailById(id);
            if (type == "json")
            {
                return Ok(result);
            }
            return null;
        }

        [Route("")]
        // POST api/modeldetailapi
        public async Task<Guid?> Post(ModelDetail modeldetail)
        {
            var result = await _dataServices.ModelDetailService.CreateModelDetail(modeldetail);
            if (!result) return null;
            //SignalR Methods Add Element
            var context = GlobalHost.ConnectionManager.GetHubContext<ModelDetailHub>();
            context.Clients.All.CreateModelDetail(modeldetail);
            var task = Task.Run(() => modeldetail.ModelDetailId);
            return await task;
        }
        [Route("{id:Guid}")]
        // PUT api/modeldetailapi/5
        public async Task<bool> Put(Guid id, ModelDetail modeldetail)
        {
            var result = await _dataServices.ModelDetailService.UpdateModelDetail(modeldetail);
            if (!result) return await Task.Run(() => false);
            modeldetail.PartNumber.ToList().ForEach(c => c.ModelDetail=null);
            //SignalR Methods Update
            var context = GlobalHost.ConnectionManager.GetHubContext<ModelDetailHub>();
            context.Clients.All.UpdateModelDetail(modeldetail);
            var task = Task.Run(() => true);
            return await task;
        }

        // DELETE api/modeldetailapi/5
        [Route("{id:Guid}")]
        public async Task<bool> Delete(Guid id)
        {
            var modeldetail = await _dataServices.ModelDetailService.GetModelDetailById(id);
            var result = await _dataServices.ModelDetailService.DeleteModelDetail(id);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Delete
            var context = GlobalHost.ConnectionManager.GetHubContext<ModelDetailHub>();
            context.Clients.All.DeleteModelDetail(modeldetail);
            var task = Task.Run(() => true);
            return await task;

        }
        [Route("PartNumber/{id:Guid}")]
        public async Task<bool> DeleteByPartNumber(Guid id)
        {
            var modeldetail = await _dataServices.ModelDetailService.GetModelDetailByPartNumberId(id);
            var result = await _dataServices.ModelDetailService.DeleteModelDetail(modeldetail.ModelDetailId);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Delete
            var context = GlobalHost.ConnectionManager.GetHubContext<ModelDetailHub>();
            context.Clients.All.DeleteModelDetail(modeldetail);
            var task = Task.Run(() => true);
            return await task;

        }
    }
}
