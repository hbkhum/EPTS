using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using EPTS.Models.Testing;
using EPTS.UI.Mobile.Views;
using Newtonsoft.Json;

namespace EPTS.UI.Mobile.ViewModel.Testing.TestPlan
{
    public class TestPlanPageViewModel: ViewModelBase
    {
        public Type Type { private set; get; }

        public string Title { private set; get; }

        public string Description { private set; get; }
        public TestGroup TestGroup { set; get; }

        private Models.Testing.TestPlan TestPlan { set; get; }



        private TestPlanPageViewModel(Type type, string title, string description, TestGroup testGroup)
        {
            Type = type;
            Title = title;
            Description = description;
            TestGroup = testGroup;
        }



        public TestPlanPageViewModel()
        {
            var client = new HttpClient { BaseAddress = new Uri("http://humbertopedraza.dynu.com/epts/webapi/api/") };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            TestPlan = Task.Run(async () =>
            {
                var response = await client.GetAsync("TestPlan/StationGroup/SAMACTIVE");
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Models.Testing.TestPlan>(data);
            }).Result;
            All = new List<TestPlanPageViewModel>();
            foreach (var testGroup in TestPlan.TestGroup)
            {
                All.Add(new TestPlanPageViewModel(typeof(AbsoluteDemoPage), testGroup.TestGroupName, testGroup.Description, testGroup));
            }


        }




        public IList<TestPlanPageViewModel> All { private set; get; }


        private void DeleteTestGroup(object message)
        {
            var testgroup = JsonConvert.DeserializeObject<TestGroup>(message.ToString());
            var rowdelete = TestPlan.TestGroup.FirstOrDefault(c => c.TestGroupId == testgroup.TestGroupId);
            Task.Run(() =>
            {
                TestPlan.TestGroup.Remove(rowdelete);
                TestPlan.TestGroup = new ObservableCollection<TestGroup>(TestPlan.TestGroup.OrderBy(c => c.Sequence));
                OnPropertyChanged("TestGroup");
            });
        }

        private void AddTestGroup(object message)
        {
            var testgroup = JsonConvert.DeserializeObject<TestGroup>(message.ToString());
            var row = new ObservableCollection<TestGroup>(TestPlan.TestGroup.Select(c => c));
            Task.Run(() =>
            {
                TestPlan.TestGroup.Add(testgroup);
                TestPlan.TestGroup = new ObservableCollection<TestGroup>(TestPlan.TestGroup.OrderBy(c => c.Sequence));
                OnPropertyChanged("TestGroup");
            });
        }

        private void UpdateTestGroup(object message)
        {
            var TestGroup = JsonConvert.DeserializeObject<TestGroup>(message.ToString());
            var row = TestPlan.TestGroup.FirstOrDefault(c => c.TestGroupId == TestGroup.TestGroupId);
            row.TestGroupName = TestGroup.TestGroupName;
            row.Description = TestGroup.Description;
            row.Sequence = TestGroup.Sequence;
            Task.Run(() =>
            {
                TestPlan.TestGroup = new ObservableCollection<TestGroup>(TestPlan.TestGroup.OrderBy(c => c.Sequence));
                OnPropertyChanged("TestGroup");
            });
        }
    }
}
