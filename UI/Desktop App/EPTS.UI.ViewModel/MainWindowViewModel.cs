

using EPTS.Core;
using EPTS.Models;
using EPTS.UI.ViewModel.Testing;

namespace EPTS.UI.ViewModel
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {

        private bool _flyourighttisopen;
        public bool FlyOutRightIsOpen
        {
            get
            {
                return _flyourighttisopen;
            }
            set
            {
                _flyourighttisopen = value;
                OnPropertyChanged("FlyOutRightIsOpen");
            }
        }
        private bool _flyouleftisopen;
        public bool FlyOutLeftIsOpen
        {
            get
            {
                return _flyouleftisopen;
            }
            set
            {
                _flyouleftisopen = value;
                OnPropertyChanged("FlyOutLeftIsOpen");
            }
        }


        public MainWindowViewModel(SettingsViewModel settingsViewModel, TestPlanViewModel testPlanViewModel)
        {
            SettingsViewModel = settingsViewModel;
            TestPlanViewModel = testPlanViewModel;
            

        }
        private DelegateCommand _settingcommand;
        public DelegateCommand SettingCommand => _settingcommand ??
                                                 (_settingcommand = new DelegateCommand(OnSetting, SettingCanExecute));

        private DelegateCommand _menucommand;
        public DelegateCommand MenuCommand => _menucommand ??
                                                 (_menucommand = new DelegateCommand(OnMenu, MenuCanExecute));

        private static bool MenuCanExecute(object obj)
        {
            return true;
        }

        private void OnMenu(object obj)
        {
            FlyOutLeftIsOpen = !FlyOutLeftIsOpen;
        }

        public SettingsViewModel SettingsViewModel { get; set; }
        public TestPlanViewModel TestPlanViewModel { get; set; }

        private void OnSetting(object parameter)
        {
            FlyOutRightIsOpen = !FlyOutRightIsOpen;
        }
        private static bool SettingCanExecute(object parameter)
        {
            return true;
        }
    }

    public interface IMainWindowViewModel
    {
        SettingsViewModel SettingsViewModel { get; set; }
        TestPlanViewModel TestPlanViewModel { get; set; }
    }


}
