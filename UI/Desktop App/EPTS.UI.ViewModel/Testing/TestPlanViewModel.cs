using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using EPTS.UI.ViewModel.Models.Testing;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace EPTS.UI.ViewModel.Testing
{
    public class TestPlanViewModel :ViewModelBase,  ITestPlanViewModel
    {
        public TestPlan TestPlan { get; set; }

        public TestPlanViewModel(TestPlan testPlan)
        {
            var client = new HttpClient {BaseAddress = new Uri("http://humbertopedraza.dynu.com/epts/webapi/api/")};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            TestPlan = Task.Run(async () =>
            {
                var response = await client.GetAsync("TestPlan/StationGroup/SAMACTIVE");
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TestPlan>(data); ;
            } ).Result;
            //TestPlan.TestGroup[0].Test.Add(new Test());


            var connection = new HubConnection("http://humbertopedraza.dynu.com/epts/WebAPI/signalr");
            //Make proxy to hub based on hub name on server
            var testHub = connection.CreateHubProxy("TestHub");
            var testGroupHub = connection.CreateHubProxy("TestGroupHub");
            //Start connection


            testHub.On<object>("UpdateTest", UpdateTest);
            testHub.On<object>("AddTest", AddTest);
            testHub.On<object>("DeleteTest", DeleteTest);

            testGroupHub.On<object>("UpdateTestGroup", UpdateTestGroup);
            testGroupHub.On<object>("AddTestGroup", AddTestGroup);
            testGroupHub.On<object>("DeleteTestGroup", DeleteTestGroup);

            //DeleteTestGroup

            connection.Start().Wait();
        }

        private void DeleteTestGroup(object message)
        {
            var testgroup = JsonConvert.DeserializeObject<TestGroup>(message.ToString());
            var rowdelete = TestPlan.TestGroup.FirstOrDefault(c => c.TestGroupId == testgroup.TestGroupId);
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                TestPlan.TestGroup.Remove(rowdelete);
                TestPlan.TestGroup = new ObservableCollection<TestGroup>(TestPlan.TestGroup.OrderBy(c => c.Sequence));
                OnPropertyChanged("TestGroup");
            }));
        }

        private void AddTestGroup(object message)
        {
            var testgroup = JsonConvert.DeserializeObject<TestGroup>(message.ToString());
            var row = new ObservableCollection<TestGroup> (TestPlan.TestGroup.Select(c => c));
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                TestPlan.TestGroup.Add(testgroup);
                TestPlan.TestGroup = new ObservableCollection<TestGroup>(TestPlan.TestGroup.OrderBy(c => c.Sequence));
                OnPropertyChanged("TestGroup");
            }));
        }

        private void UpdateTestGroup(object message)
        {
            var TestGroup = JsonConvert.DeserializeObject<TestGroup>(message.ToString());
            var row = TestPlan.TestGroup.FirstOrDefault(c => c.TestGroupId == TestGroup.TestGroupId);
            row.TestGroupName = TestGroup.TestGroupName;
            row.Description = TestGroup.Description;
            row.Sequence = TestGroup.Sequence;
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                TestPlan.TestGroup = new ObservableCollection<TestGroup>(TestPlan.TestGroup.OrderBy(c => c.Sequence));
                OnPropertyChanged("TestGroup");
            }));
        }

        private void DeleteTest(object message)
        {
            var test = JsonConvert.DeserializeObject<Test>(message.ToString());
            var rowdelete = TestPlan.TestGroup.FirstOrDefault(c => c.TestGroupId == test.TestGroupId).Test.FirstOrDefault(c => c.TestId == test.TestId);
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                TestPlan.TestGroup.FirstOrDefault(c => c.TestGroupId == test.TestGroupId).Test.Remove(rowdelete);
                TestPlan.TestGroup.FirstOrDefault(c => c.TestGroupId == test.TestGroupId).Test = new ObservableCollection<Test>(TestPlan.TestGroup.FirstOrDefault(c => c.TestGroupId == test.TestGroupId).Test.OrderBy(c => c.Sequence));
                OnPropertyChanged("Test");
            }));
        }

        private void AddTest(object message)
        {
            var test = JsonConvert.DeserializeObject<Test>(message.ToString());
            test.TestTypeName = test.TestType.TestTypeName;
            test.TestUnitName = test.TestUnit.TestUnitName;
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                TestPlan.TestGroup.FirstOrDefault(c => c.TestGroupId == test.TestGroupId).Test.Add(test);
                TestPlan.TestGroup.FirstOrDefault(c => c.TestGroupId == test.TestGroupId).Test = new ObservableCollection<Test>(TestPlan.TestGroup.FirstOrDefault(c => c.TestGroupId == test.TestGroupId).Test.OrderBy(c => c.Sequence));
                OnPropertyChanged("Test");
            }));
        }

        private void UpdateTest(object message)
        {
            var test = JsonConvert.DeserializeObject<Test>(message.ToString());
            test.TestTypeName = test.TestType.TestTypeName;
            test.TestUnitName = test.TestUnit.TestUnitName;
            var row = TestPlan.TestGroup.FirstOrDefault(c => c.TestGroupId == test.TestGroupId).Test.FirstOrDefault(c => c.TestId == test.TestId);
            row.HiLimit = test.HiLimit;
            row.TestType = test.TestType;
            row.TestTypeName = test.TestTypeName;
            row.TestUnit = test.TestUnit;
            row.Result = test.Result;
            row.FinishTime = test.FinishTime;
            row.LoLimit = test.LoLimit;
            row.Sequence = test.Sequence;
            row.TestName = test.TestName;
            row.TestDesciption = test.TestDesciption;
            row.TestUnitName = test.TestUnitName;
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                TestPlan.TestGroup.FirstOrDefault(c => c.TestGroupId == test.TestGroupId).Test = new ObservableCollection<Test>(TestPlan.TestGroup.FirstOrDefault(c => c.TestGroupId == test.TestGroupId).Test.OrderBy(c => c.Sequence));
                OnPropertyChanged("Test");
            }));
        }
    }

    public interface ITestPlanViewModel
    {
        TestPlan TestPlan { set; get; }
    }
}
