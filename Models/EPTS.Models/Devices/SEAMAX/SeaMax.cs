
using EPTS.Models.Devices.Core;

namespace EPTS.Models.Devices.SEAMAX
{
    public abstract class SeaMax : DeviceInformation
    {
        //[AlsoNotifyFor("Com")]
        public string Com { get; set; }
        //[AlsoNotifyFor("SlaveId")]
        public int SlaveId { get; set; }
    }
}