using EPTS.UI.Mobile.ViewModel.Testing;

namespace EPTS.UI.Mobile.ViewModel
{
    public class ViewModelLocator
    {
        private  MainPageViewModel _mainPageViewModel;
        public  MainPageViewModel MainPageViewModelStatic
        {
            get
            {

                if (_mainPageViewModel != null) return _mainPageViewModel;
                _mainPageViewModel= new MainPageViewModel(new TestPlanViewModel());
                return _mainPageViewModel;
            }
        }
    }

}
