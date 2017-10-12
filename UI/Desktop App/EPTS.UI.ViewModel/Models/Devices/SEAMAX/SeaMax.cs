
using DeviceInformation = EPTS.UI.ViewModel.Models.Devices.Core.DeviceInformation;

namespace EPTS.UI.ViewModel.Models.Devices.SEAMAX
{
    public abstract class SeaMax : DeviceInformation
    {
        //[AlsoNotifyFor("Com")]
        public string Com { get; set; }
        //[AlsoNotifyFor("SlaveId")]
        public int SlaveId { get; set; }
    }
}