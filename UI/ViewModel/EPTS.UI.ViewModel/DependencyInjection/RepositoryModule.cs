using EPTS.Repositories.XML.Devices.Repositories;
using Ninject.Modules;

namespace EPTS.UI.ViewModel.DependencyInjection
{
    public class RepositoryModule : NinjectModule
    {
        public override void Load()
        {
            //Bind<IDataRepositories>()
            //    .To<DataRepositories>().WithConstructorArgument("filelocation", @"~\Infrastructure\Data\Devices.xml");
        }
    }
}
