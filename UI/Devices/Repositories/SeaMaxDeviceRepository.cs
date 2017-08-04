using System;
using System.Collections.Generic;

using Devices.SEAMAX;

namespace Devices.Repositories
{
    public interface ISeaMaxDeviceRepository
    {
        List<SeaLevel420Device> SeaLevel420Device { get; set; }
        List<SeaLevel410Device> SeaLevel410Device { get; set; }
    }
    public class SeaMaxDeviceRepository : ISeaMaxDeviceRepository
    {
        public List<SeaLevel420Device> SeaLevel420Device { get; set; }
        public List<SeaLevel410Device> SeaLevel410Device { get; set; }
    }
}
