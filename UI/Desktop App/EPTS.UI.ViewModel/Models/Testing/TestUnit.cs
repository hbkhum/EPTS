using System;


namespace EPTS.UI.ViewModel.Models.Testing
{
    public class TestUnit:ViewModelBase
    {
        private Guid _testUnitId;
        private string _testUnitName;
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
    }
}