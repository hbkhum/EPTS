using System;
using PropertyChanged;

namespace EPTS.UI.WPF.Models.Testing
{
    public class TestGroupLink
    {
        [AlsoNotifyFor("TestGroupId")]
        public Guid TestGroupLinkId { get; set; }

        [AlsoNotifyFor("TestGroup")]
        public virtual TestGroup TestGroup { get; set; }

        [AlsoNotifyFor("Sequence")]
        public int Sequence { get; set; }

        [AlsoNotifyFor("Description")]
        public string Description { get; set; }
    }
}