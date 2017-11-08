using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EPTS.UI.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class XamlPlusCodePage : ContentPage
    {
        public XamlPlusCodePage()
        {
            InitializeComponent();
        }
        void OnSliderValueChanged(object sender,
                          ValueChangedEventArgs args)
        {
            valueLabel.Text = args.NewValue.ToString("F3");
        }

        async void OnButtonClicked(object sender, EventArgs args)
        {
            Button button = (Button)sender;
            await DisplayAlert("Clicked!",
                "The button labeled '" + button.Text + "' has been clicked",
                "OK");
        }
    }
}