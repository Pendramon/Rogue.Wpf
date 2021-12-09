using Rogue.Wpf.Commands;
using Rogue.Wpf.ViewModels.Base;
using Rogue.Wpf.ViewModels.Interfaces;
using Rogue.Wpf.ViewModels.MainContent.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Rogue.Wpf.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IMainWindowViewModel
    {
        private string title = "Rogue";

        public string Title
        {
            get => title;
            set
            {
                if (title != value)
                    OnPropertyChanged();

                title = value;
            }
        }

        private List<IViewModel> viewModelsList { get; }

        private IViewModel previousMainContentViewModel;

        private IViewModel currentMainContentViewModel;

        public IViewModel CurrentMainContentViewModel
        {
            get => currentMainContentViewModel;
            set
            {
                if (value == currentMainContentViewModel) return;

                previousMainContentViewModel = currentMainContentViewModel;
                currentMainContentViewModel = value;
                OnPropertyChanged();
            }
        }

        #region Commands

        public ICommand ToggleThemeEditorCommand { get; set; }

        #endregion

        public MainWindowViewModel(IMainContentViewModel mainContentViewModel, IThemeEditorViewModel themeEditorViewModel)
        {
            this.viewModelsList = new List<IViewModel>()
            {
                mainContentViewModel,
                themeEditorViewModel
            };
            this.SetDefaultViewModel();

            ToggleThemeEditorCommand = new RelayCommand(ToggleThemeEditor);
        }

        private void SetDefaultViewModel() => this.CurrentMainContentViewModel =
            viewModelsList.FirstOrDefault(x => x is IMainContentViewModel);

        private void ToggleThemeEditor() => this.CurrentMainContentViewModel =
            CurrentMainContentViewModel is IThemeEditorViewModel
                ? previousMainContentViewModel
                : viewModelsList.FirstOrDefault(x => x is IThemeEditorViewModel);

    }
}
