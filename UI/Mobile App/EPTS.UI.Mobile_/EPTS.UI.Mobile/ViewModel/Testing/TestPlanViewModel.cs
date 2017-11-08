using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Input;
using EPTS.Models;
using EPTS.Models.Testing;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Test = EPTS.UI.Mobile.Views.Test;

namespace EPTS.UI.Mobile.ViewModel.Testing
{
    public class TestPlanViewModel: ViewModelBase, ITestPlanViewModel
    {
        public TestPlan TestPlan { get; set; }

        private ICommand _viewCommand;

        public ICommand ViewCommand => _viewCommand ??
                                         (_viewCommand = new Command(CalculateSquareRoot));

        private TestGroup _selectedTestGroup;
        public TestGroup SelectedTestGroup
        {
            get
            {
                return _selectedTestGroup;
            }

            set
            {
                _selectedTestGroup = value;
                OnPropertyChanged("SelectedTestGroup");
            }
        }

        private void CalculateSquareRoot(object obj)
        {
            throw new NotImplementedException();
        }

        public TestPlanViewModel()
        {
            var client = new HttpClient { BaseAddress = new Uri("http://humbertopedraza.dynu.com/epts/webapi/api/") };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            TestPlan = Task.Run(async () =>
            {
                var response = await client.GetAsync("TestPlan/StationGroup/SAMACTIVE");
                var data = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TestPlan>(data) ;
            }).Result;
        }
    }

    public interface ITestPlanViewModel
    {
        TestPlan TestPlan { set; get; }
        TestGroup SelectedTestGroup { set; get; }

    }
}
