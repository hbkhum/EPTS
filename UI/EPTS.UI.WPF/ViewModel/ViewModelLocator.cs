
using EPTS.UI.WPF.DependencyInjection;
using Ninject;


namespace EPTS.UI.WPF.ViewModel
{
    public class ViewModelLocator
    {
        private static MainWindowViewModel _mainWindowViewModel;
        public static MainWindowViewModel MainWindowViewModelStatic
        {
            get
            {
                if (_mainWindowViewModel != null) return _mainWindowViewModel;
                var kernel = new StandardKernel(new RepositoryViewModel());
                _mainWindowViewModel = kernel.Get<MainWindowViewModel>();
                return _mainWindowViewModel;
            }
        }
    }

}
