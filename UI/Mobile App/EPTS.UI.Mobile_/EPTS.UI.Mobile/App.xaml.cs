using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace EPTS.UI.Mobile
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new EPTS.UI.Mobile.MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts

        }


        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
