using Rogue.Wpf.Models;
using Rogue.Wpf.Models.Interfaces;
using Rogue.Wpf.ViewModels.Base;
using Rogue.Wpf.ViewModels.MainContent.Interfaces;

namespace Rogue.Wpf.ViewModels.MainContent
{
    public class ThemeEditorViewModel : ViewModelBase, IThemeEditorViewModel
    {
        private readonly IThemeService themeService;

        private string themeName;

        public string ThemeName
        {
            get => themeName;
            set
            {
                if (value == themeName) return;

                themeName = value;
                OnPropertyChanged();
            }
        }

        public ThemeEditorViewModel(IThemeService themeService)
        {
            this.themeService = themeService;
            this.themeService.ThemeChanged += ThemeService_ThemeChanged;
        }

        private void ThemeService_ThemeChanged(object sender, ThemeChangedEventArgs eventArgs)
        {
            this.ThemeName = eventArgs.NewTheme.Name;
        }
    }
}
