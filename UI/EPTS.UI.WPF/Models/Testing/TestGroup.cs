using System;
using System.Collections.Generic;
using PropertyChanged;

namespace EPTS.UI.WPF.Models.Testing
{
    public class TestGroup
    {
        [AlsoNotifyFor("TestGroupId")]
        public Guid TestGroupId { get; set; }

        [AlsoNotifyFor("TestGroupName")]
        public string TestGroupName { get; set; }

        public virtual ICollection<TestGroupLink> TestGroupLink { get; set; }

        public virtual ICollection<TestPlanLink> TestPlanLink { get; set; }

    }
}