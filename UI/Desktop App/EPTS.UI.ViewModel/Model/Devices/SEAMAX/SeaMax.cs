
namespace EPTS.UI.ViewModel.Model.Devices.SEAMAX
{
    public abstract class SeaMax : Model.Devices.Core.DeviceInformation
    {
        //[AlsoNotifyFor("Com")]
        public string Com { get; set; }
        //[AlsoNotifyFor("SlaveId")]
        public int SlaveId { get; set; }
    }
}