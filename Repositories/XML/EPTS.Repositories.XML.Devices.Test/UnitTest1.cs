
using System.Collections.Generic;
using EPTS.Repositories.XML.Devices.Infrastructure.Entities.SEAMAX;
using EPTS.Repositories.XML.Devices.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject;

namespace EPTS.Repositories.XML.Devices.Test
{
    
    [TestClass]
    public class UnitTest1
    {
        private IKernel _kernel;
        [TestInitialize]
        public void Init()
        {
            _kernel = new StandardKernel();
            //_kernel.Bind<XmlContext>().ToSelf().WithConstructorArgument("appSettings", @"D:\Users\HBK\Source\Workspaces\EPTS\Repositories\XML\EPTS.Repositories.XML.Devices\Infrastructure\Data\Devices.xml");
            _kernel.Bind<DataRepositories>().ToSelf().WithConstructorArgument("filelocation", "Infrastructure/Data/Devices.xml");

            var s = _kernel.Get<DataRepositories>();
            //s.Dev.SeaMaxDeviceRepository.SeaIO420[0].SlaveId = "666";
            s.Save();
            //var a = s.Device.SeaMaxDevice
        }
        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
