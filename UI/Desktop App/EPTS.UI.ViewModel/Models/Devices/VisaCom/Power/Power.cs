
namespace EPTS.UI.ViewModel.Models.Devices.VisaCom.Power
{
    public class Power : Visa
    {
        //[AlsoNotifyFor("Current")]
        public string Current { get; set; }

        //[AlsoNotifyFor("Voltage")]
        public string Voltage { get; set; }
    }
}
