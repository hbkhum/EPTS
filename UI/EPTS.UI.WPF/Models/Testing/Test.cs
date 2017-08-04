

using System;
using System.Collections.Generic;
using System.ComponentModel;
using EPTS.UI.WPF.ViewModel;
using PropertyChanged;


namespace EPTS.UI.WPF.Models.Testing
{
    public class Test : ViewModelBase
    {
        [AlsoNotifyFor("TestId")]
        public Guid TestId { get; set; }
        [AlsoNotifyFor("TestGroup")]
        public virtual TestGroup TestGroup { get; set; }
        [AlsoNotifyFor("TestName")]
        public string TestName { get; set; }
        [AlsoNotifyFor("TestDesciption")]
        public string TestDesciption { get; set; }
        public virtual ICollection<TestLink> TestLink { get; set; }
    }
}