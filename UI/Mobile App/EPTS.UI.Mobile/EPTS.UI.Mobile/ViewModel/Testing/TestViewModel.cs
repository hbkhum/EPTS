using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPTS.Models.Testing;

namespace EPTS.UI.Mobile.ViewModel.Testing
{
    public class TestViewModel
    {
        public Test Test { set; get; }
        public TestViewModel(Test test)
        {
            Test = test;
        }
    }
}
