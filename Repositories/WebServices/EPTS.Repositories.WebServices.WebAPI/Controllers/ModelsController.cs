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
    [HubName("ModelHub")]
    public class ModelHub : Hub { }
    [RoutePrefix("api/Model")]
    public class ModelsController : ApiControllerWithHub<ModelHub>
    {
        private readonly IDataServices _dataServices;

        public ModelsController(IDataServices dataServices)
        {
            _dataServices = dataServices;

        }

        // GET api/modelapi
        [Route("{pageSize:int?}/{pageNumber:int?}/{orderby:alpha?}/{wherevalue:alpha?}/{type:alpha?}")]
        public async Task<IHttpActionResult> Get(int pageSize = 5000, int pageNumber = 1, string whereValue = "", string orderBy = "", string type = "json")
        {
            whereValue = whereValue ?? "";
            var result = await _dataServices.ModelService.GetAllModels(whereValue, orderBy);
            var data = result as IList<Model> ?? result.ToList();
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

        // GET api/modelapi/5
        [Route("{id:Guid}")]
        public async Task<IHttpActionResult> Get(Guid id, string type = "json")
        {
            var result = await _dataServices.ModelService.GetModelById(id);
            if (type == "json")
            {
                return Ok(result);
            }
            return null;
        }


        // POST api/modelapi
        [Route("")]
        public async Task<Guid?> Post(Model model)
        {
            var result = await _dataServices.ModelService.CreateModel(model);
            if (!result) return null;
            //SignalR Methods Add Element
            model.Family = await _dataServices.FamilyService.GetFamilyById(model.FamilyId);
            model.Family.BusinessUnit = await _dataServices.BusinessUnitService.GetBusinessUnitById(model.Family.BusinessUnitId);
            var context = GlobalHost.ConnectionManager.GetHubContext<ModelHub>();
            context.Clients.All.AddModel(model);
            var task = Task.Run(() => model.ModelId);
            return await task;
        }

        // PUT api/modelapi/5
        [Route("{id:Guid}")]
        public async Task<bool> Put(Guid id, Model model)
        {
            model = new Model
            {
                ModelId = model.ModelId,
                FamilyId = model.FamilyId,
                ModelName = model.ModelName,

            };
            var result = await _dataServices.ModelService.UpdateModel(model);
            if (!result) return await Task.Run(() => false);
            ;
            //SignalR Methods Update
            var context = GlobalHost.ConnectionManager.GetHubContext<ModelHub>();
            context.Clients.All.UpdateModel(model);
            var task = Task.Run(() => true);
            return await task;
        }

        // DELETE api/modelapi/5
        [Route("{id:Guid}")]
        public async Task<bool> Delete(Guid id)
        {
            var model = await _dataServices.ModelService.GetModelById(id);
            var result = await _dataServices.ModelService.DeleteModel(id);
            if (!result) return await Task.Run(() => false);
            //SignalR Methods Delete
            var context = GlobalHost.ConnectionManager.GetHubContext<ModelHub>();
            context.Clients.All.DeleteModel(model);
            var task = Task.Run(() => true);
            return await task;

        }
    }
}
