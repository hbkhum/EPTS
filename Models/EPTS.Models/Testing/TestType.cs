using System;

namespace EPTS.Models.Testing
{
    public class TestType:ViewModelBase
    {
        private Guid _testTypeId;
        private string _testTypeName;
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
    }
}