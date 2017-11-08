using EPTS.UI.Mobile.ViewModel.Testing;
using EPTS.UI.Mobile.ViewModel.Testing.TestPlan;

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
                _mainPageViewModel= new MainPageViewModel(new TestPlanPageViewModel());
                return _mainPageViewModel;
            }
        }
    }

}
