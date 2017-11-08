using System;
using System.Collections.ObjectModel;

namespace EPTS.UI.ViewModel.Model.Testing
{

    public class TestGroup:ViewModelBase
    {
        private Guid _testGroupId;
        private string _testGroupName;
        private TestPlan _testPlan;
        private Guid _testPlanId;
        private string _description;
        private int _sequence;
        private ObservableCollection<Test> _test;
        //[AlsoNotifyFor("TestGroupId")]
        public Guid TestGroupId
        {
            get { return _testGroupId; }
            set
            {
                _testGroupId = value;
                OnPropertyChanged("TestGroupId");
            }
        }

        //[AlsoNotifyFor("TestGroupName")]
        public string TestGroupName
        {
            get { return _testGroupName; }
            set
            {
                _testGroupName = value;
                OnPropertyChanged("TestGroupName");
            }
        }

        //[AlsoNotifyFor("TestPlan")]
        public TestPlan TestPlan
        {
            get { return _testPlan; }
            set
            {
                _testPlan = value;
                OnPropertyChanged("TestPlan");
            }
        }

        //[AlsoNotifyFor("TestPlanId")]
        public Guid TestPlanId
        {
            get { return _testPlanId; }
            set
            {
                _testPlanId = value;
                OnPropertyChanged("TestPlanId");
            }
        }

        //[AlsoNotifyFor("Description")]
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        //[AlsoNotifyFor("Sequence")]
        public int Sequence
        {
            get { return _sequence; }
            set
            {
                _sequence = value;
                OnPropertyChanged("Sequence");
            }
        }

        //[AlsoNotifyFor("TestGroupNameInformation")]
        public string TestGroupNameInformation => TestGroupName + ", " + Description + ", " + Sequence.ToString();

        //[AlsoNotifyFor("Test")]
        public virtual ObservableCollection<Test> Test
        {
            get { return _test; }
            set
            {
                _test = value;
                OnPropertyChanged("Test");
            }
        }
    }
}