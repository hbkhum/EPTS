using System.Data.Entity;
using EPTS.Repositories.WebServices.WebAPI.Repositories;
using EPTS.Repositories.WebServices.WebAPI.Infrastructure.Context;
using Ninject.Modules;
using Ninject.Web.Common;

namespace EPTS.Repositories.WebServices.WebAPI.DependencyInjection
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<EptsContext>().InRequestScope();
            Bind<IDataRepositories>().To<DataRepositories>();
        }
    }
}