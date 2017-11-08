using System;
using System.Collections.ObjectModel;

namespace EPTS.Models.Testing
{
    public class TestPlan: ViewModelBase
    {
        private Guid _testPlanId;
        private string _testPlanName;
        private string _desciption;
        private ObservableCollection<TestGroup> _testGroup;
        //[AlsoNotifyFor("TestPlanId")]
        public Guid TestPlanId
        {
            get { return _testPlanId; }
            set {
                _testPlanId = value;
                OnPropertyChanged("TestPlanId");
            }
        }

        //[AlsoNotifyFor("TestPlanName")]
        public string TestPlanName
        {
            get { return _testPlanName; }
            set
            {
                _testPlanName = value;
                OnPropertyChanged("TestPlanName");
            }
        }

        //[AlsoNotifyFor("Desciption")]
        public string Desciption
        {
            get { return _desciption; }
            set
            {
                _desciption = value;
                OnPropertyChanged("Desciption");
            }
        }

        //[AlsoNotifyFor("TestGroup")]
        public virtual ObservableCollection<TestGroup> TestGroup
        {
            get { return _testGroup; }
            set
            {
                _testGroup = value;
                OnPropertyChanged("TestGroup");
            }
        }
    }
}