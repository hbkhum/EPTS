using System;
using System.Collections.Generic;
using PropertyChanged;

namespace EPTS.UI.WPF.Models.Testing
{
    public class TestPlan
    {
        [AlsoNotifyFor("TestPlanId")]
        public Guid TestPlanId { get; set; }

        [AlsoNotifyFor("TestPlanName")]
        public string TestPlanName { get; set; }

        [AlsoNotifyFor("TestPlanLink")]
        public virtual ICollection<TestPlanLink> TestPlanLink { get; set; }

    }
}