using EPTS.UI.Mobile.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPTS.UI.Mobile.ViewModel.Testing;
using EPTS.UI.Mobile.ViewModel.Testing.TestPlan;
using Xamarin.Forms;

namespace EPTS.UI.Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ((ListView) sender).SelectedItem = null;

            if (args.SelectedItem == null) return;
            if (!(args.SelectedItem is TestPlanPageViewModel pageData)) return;
            var page = (Page)Activator.CreateInstance(pageData.Type);
            page.BindingContext = pageData.TestGroup;
            await Navigation.PushAsync(page);
        }
    }
}
