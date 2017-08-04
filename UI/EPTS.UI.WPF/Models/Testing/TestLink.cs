using System;
using EPTS.UI.WPF.Models.Testing;
using PropertyChanged;

namespace EPTS.UI.WPF.Models.Testing
{
    public class TestLink
    {
        [AlsoNotifyFor("TestLinkId")]
        public Guid TestLinkId { get; set; }

        [AlsoNotifyFor("Test")]
        public virtual Test Test { get; set; }

        [AlsoNotifyFor("LoLimit")]
        public string LoLimit { get; set; }

        [AlsoNotifyFor("HiLimit")]
        public string HiLimit { get; set; }

        [AlsoNotifyFor("TestUnit")]
        public virtual TestUnit TestUnit { get; set; }

        [AlsoNotifyFor("TestType")]
        public virtual TestType TestType { get; set; }
    }
}