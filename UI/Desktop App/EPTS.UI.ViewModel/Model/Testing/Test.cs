using System;

namespace EPTS.UI.ViewModel.Model.Testing
{
    public class Test :ViewModelBase
    {
        private Guid _testId;
        private TestGroup _testGroup;
        private string _testName;
        private Guid _testGroupId;
        private string _testDesciption;
        private int _sequence;
        private string _loLimit;
        private Guid _testUnitId;
        private TestType _testType;
        private string _hiLimit;
        private TestUnit _testUnit;
        private Guid _testTypeId;
        private string _status;
        private string _result;
        private DateTime? _starTime;
        private string _testUnitName;
        private string _testTypeName;
        private DateTime? _finishTime;
        //[AlsoNotifyFor("TestId")]
        public Guid TestId
        {
            get { return _testId; }
            set
            {
                _testId = value;
                OnPropertyChanged("TestId");
            }
        }

        //[AlsoNotifyFor("TestGroup")]
        public virtual TestGroup TestGroup
        {
            get { return _testGroup; }
            set
            {
                _testGroup = value;
                OnPropertyChanged("TestGroup");
            }
        }

        //[AlsoNotifyFor("TestName")]
        public string TestName
        {
            get { return _testName; }
            set
            {
                _testName = value;
                OnPropertyChanged("TestName");
            }
        }

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

        //[AlsoNotifyFor("TestDesciption")]
        public string TestDesciption
        {
            get { return _testDesciption; }
            set
            {
                _testDesciption = value;
                OnPropertyChanged("TestDesciption");
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

        //[AlsoNotifyFor("LoLimit")]
        public string LoLimit
        {
            get { return _loLimit; }
            set
            {
                _loLimit = value;
                OnPropertyChanged("LoLimit");
            }
        }

        //[AlsoNotifyFor("TestUnitId")]
        public Guid TestUnitId
        {
            get { return _testUnitId; }
            set
            {
                _testUnitId = value;
                OnPropertyChanged("TestUnitId");
            }
        }

        //[AlsoNotifyFor("TestType")]
        public TestType TestType
        {
            get { return _testType; }
            set
            {
                _testType = value;
                OnPropertyChanged("TestType");
            }
        }

        //[AlsoNotifyFor("HiLimit")]
        public string HiLimit
        {
            get { return _hiLimit; }
            set
            {
                _hiLimit = value;
                OnPropertyChanged("HiLimit");
            }
        }

        //[AlsoNotifyFor("TestUnit")]
        public TestUnit TestUnit
        {
            get { return _testUnit; }
            set
            {
                _testUnit = value;
                OnPropertyChanged("TestUnit");
            }
        }

        //[AlsoNotifyFor("TestTypeId")]
        public Guid TestTypeId
        {
            get { return _testTypeId; }
            set
            {
                _testTypeId = value;
                OnPropertyChanged("TestTypeId");
            }
        }

        //[AlsoNotifyFor("Status")]
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        //[AlsoNotifyFor("Result")]
        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                OnPropertyChanged("Result");
            }
        }

        //[AlsoNotifyFor("StarTime")]
        public DateTime? StarTime
        {
            get { return _starTime; }
            set
            {
                _starTime = value;
                OnPropertyChanged("StarTime");
            }
        }

        //[AlsoNotifyFor("TestUnitName")]
        public string TestUnitName
        {
            get { return _testUnitName; }
            set
            {
                _testUnitName = value;
                OnPropertyChanged("TestUnitName");
            }
        }

        //[AlsoNotifyFor("TestTypeName")]
        public string TestTypeName
        {
            get { return _testTypeName; }
            set
            {
                _testTypeName = value;
                OnPropertyChanged("TestTypeName");
            }
        }

        //[AlsoNotifyFor("FinishTime")]
        public DateTime? FinishTime
        {
            get { return _finishTime; }
            set
            {
                _finishTime = value;
                OnPropertyChanged("FinishTime");
            }
        }
    }
}