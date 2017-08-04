using System.Dynamic;
using EPTS.Repositories.XML.Devices.Repositories;
using EPTS.UI.ViewModel.Devices;
using EPTS.UI.ViewModel.Devices.SEAMAX;
using Ninject;
using Ninject.Modules;

namespace EPTS.UI.ViewModel
{
    public class ViewModelLocator
    {
        private static MainWindowViewModel _mainWindowViewModel;
        //public static MainWindowViewModel
        //    MainWindowViewModelStatic => _mainWindowViewModel ?? (_mainWindowViewModel = new MainWindowViewModel());
        public static MainWindowViewModel MainWindowViewModelStatic
        {
            get
            {
                if (_mainWindowViewModel != null) return _mainWindowViewModel;
                var kernel = new StandardKernel(new RunTimeModule());
                _mainWindowViewModel = kernel.Get<MainWindowViewModel>();
                return _mainWindowViewModel;
            }
        }
        public ViewModelLocator()
        {

            //var t = kernel.Get<DeviceViewModel>();
            //t.SeaMaxViewModel.SeaMaxDeviceRepository.SeaIO420
        }
    }
    public class RunTimeModule : NinjectModule
    {
        public override void Load()
        {
            Bind<MainWindowViewModel>().ToSelf().InSingletonScope();
            Bind<IDataRepositories>().To<DataRepositories>().WithConstructorArgument("filelocation", "Infrastructure/Data/Devices.xml");
            Bind<IDeviceViewModel>().To<DeviceViewModel>();
            Bind<IMainWindowViewModel>().To<MainWindowViewModel>();
        }
    }
}
