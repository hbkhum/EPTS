using PropertyChanged;

namespace EPTS.UI.WPF.Models.Devices.VisaCom.Com.Scanner
{

    public class Scanner : Com
    {
        [AlsoNotifyFor("Trigger")]
        public string Trigger { get; set; }
    }
}