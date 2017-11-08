using EPTS.Repositories.WebServices.WebAPI.Services;
using Ninject.Modules;

namespace EPTS.Repositories.WebServices.WebAPI.DependencyInjection
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataServices>().To<DataServices>();
        }
    }
}