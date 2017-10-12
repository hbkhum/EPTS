using EPTS.Repositories.XML.Devices.Repositories;
using EPTS.UI.ViewModel;
using EPTS.UI.ViewModel.Testing;
using Ninject.Modules;

namespace EPTS.UI.ViewModel.DependencyInjection
{
    public class RepositoryViewModel : NinjectModule
    {
        public override void Load()
        {
            Bind<MainWindowViewModel>().ToSelf().InSingletonScope();
            Bind<IDataRepositories>().To<DataRepositories>().WithConstructorArgument("filelocation", "Infrastructure/Data/Devices.xml");
            Bind<ITestPlanViewModel>().To<TestPlanViewModel>();
            Bind<ISettingsViewModel>().To<SettingsViewModel>();
            Bind<IMainWindowViewModel>().To<MainWindowViewModel>();
        }
    }
}
