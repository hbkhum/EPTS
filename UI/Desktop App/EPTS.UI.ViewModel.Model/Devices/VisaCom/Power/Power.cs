
namespace EPTS.UI.ViewModel.Model.Devices.VisaCom.Power
{
    public class Power : Visa
    {
        //[AlsoNotifyFor("Current")]
        public string Current { get; set; }

        //[AlsoNotifyFor("Voltage")]
        public string Voltage { get; set; }
    }
}
