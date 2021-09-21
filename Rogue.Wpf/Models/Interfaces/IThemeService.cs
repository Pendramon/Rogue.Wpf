using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Rogue.Wpf.Models.Interfaces
{
    public interface IThemeService
    {
        Theme ActiveTheme { get; }
        List<Theme> DefaultThemes { get; }
        List<Theme> CustomThemes { get; }
        void SetTheme(Theme theme);
        Task InitializeAsync();

        event EventHandler<ThemeChangedEventArgs> ThemeChanged;
        // TODO: Create Theme
    }
}
