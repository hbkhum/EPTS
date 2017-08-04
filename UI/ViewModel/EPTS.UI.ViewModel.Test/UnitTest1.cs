using System;
using EPTS.Repositories.XML.Devices.Repositories;
using EPTS.UI.ViewModel.Devices;
using EPTS.UI.ViewModel.Devices.SEAMAX;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace EPTS.UI.ViewModel.Test
{
    [TestClass]
    public class UnitTest1
    {
        private IKernel _kernel;
        [TestInitialize]
        public void Init()
        {
            _kernel = new StandardKernel();
            _kernel.Bind<IDataRepositories>().To<DataRepositories>().WithConstructorArgument("filelocation", "Infrastructure/Data/Devices.xml");
            _kernel.Bind<IDeviceViewModel>().To<DeviceViewModel>();
            _kernel.Bind<IMainWindowViewModel>().To<MainWindowViewModel>();
            _kernel.Bind<ISettingsViewModel>().To<SettingsViewModel>();
            var t = _kernel.Get<DeviceViewModel>();
            //s.Dev.SeaMaxDeviceRepository.SeaIO420[0].SlaveId = "666";
            //var a = s.Device.SeaMaxDevice
        }
        [TestMethod]
        public void TestMethod2()
        {
        }
    }
}
