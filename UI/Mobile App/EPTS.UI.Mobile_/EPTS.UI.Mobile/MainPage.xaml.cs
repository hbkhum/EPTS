
using System.Windows.Input;
using EPTS.UI.Mobile.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EPTS.UI.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.BindingContext = new ViewModelLocator().MainPageViewModelStatic;
            InitializeComponent();

        }

    }


}