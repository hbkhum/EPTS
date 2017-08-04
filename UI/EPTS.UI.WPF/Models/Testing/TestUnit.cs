using System;
using PropertyChanged;

namespace EPTS.UI.WPF.Models.Testing
{
    public class TestUnit
    {
        [AlsoNotifyFor("TestUnitId")]
        public Guid TestUnitId { get; set; }

        [AlsoNotifyFor("TestUnitName")]
        public string TestUnitName { get; set; }
    }
}