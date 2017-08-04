using System;
using PropertyChanged;

namespace EPTS.UI.WPF.Models.Testing
{
    public class TestType
    {
        [AlsoNotifyFor("TestTypeId")]
        public Guid TestTypeId { get; set; }

        [AlsoNotifyFor("TestTypeName")]
        public string TestTypeName { get; set; }
    }
}