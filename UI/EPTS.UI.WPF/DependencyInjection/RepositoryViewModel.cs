using EPTS.Repositories.XML.Devices.Repositories;
using EPTS.UI.WPF.ViewModel;
using EPTS.UI.WPF.ViewModel.Devices;
using Ninject.Modules;

namespace EPTS.UI.WPF.DependencyInjection
{
    public class RepositoryViewModel : NinjectModule
    {
        public override void Load()
        {
            Bind<MainWindowViewModel>().ToSelf().InSingletonScope();
            Bind<IDataRepositories>().To<DataRepositories>().WithConstructorArgument("filelocation", "Infrastructure/Data/Devices.xml");
            Bind<ISettingsViewModel>().To<SettingsViewModel>();
            Bind<IMainWindowViewModel>().To<MainWindowViewModel>();
        }
    }
}
