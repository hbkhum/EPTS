using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using EPTS.Models;
using EPTS.UI.Mobile.ViewModel.Testing;
using EPTS.UI.Mobile.ViewModel.Testing.TestPlan;
using Xamarin.Forms;


namespace EPTS.UI.Mobile.ViewModel
{
    public class MainPageViewModel : ViewModelBase,IMainPageViewModel
    {
        public TestPlanPageViewModel TestPlanPageViewModel { get; set; }
        public MainPageViewModel(TestPlanPageViewModel testPlanPageViewModel)
        {
            TestPlanPageViewModel = testPlanPageViewModel;
        }
        //private DelegateCommand _nextPagecommand;
        //public DelegateCommand NextPageCommand => _nextPagecommand ??
        //                                         (_nextPagecommand = new DelegateCommand(OnNextPage, NextPageCanExecute));

        //private static bool NextPageCanExecute(object obj)
        //{
        //    //await this.na .Navigation.PushAsync(page);
        //    return true;
        //}

        //private void OnNextPage(object obj)
        //{
        //    //FlyOutLeftIsOpen = !FlyOutLeftIsOpen;
        //}
    }

    public interface IMainPageViewModel
    {
        TestPlanPageViewModel TestPlanPageViewModel { get; set; }
    }
}
