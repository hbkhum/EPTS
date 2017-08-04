using System;
using PropertyChanged;

namespace EPTS.UI.WPF.Models.Testing
{
    public class TestPlanLink
    {
        [AlsoNotifyFor("TestPlanLinkId")]
        public Guid TestPlanLinkId { get; set; }

        [AlsoNotifyFor("TestGroup")]
        public virtual TestGroup TestGroup { get; set; }
        [AlsoNotifyFor("TestPlan")]
        public virtual TestPlan TestPlan { get; set; }
    }
}