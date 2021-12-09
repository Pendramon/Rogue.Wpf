using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rogue.Wpf.Models.Interfaces
{
    public interface IThemeService
    {
        Theme ActiveTheme { get; }
        List<Theme> Themes { get; }
        void SetTheme(Theme theme);
        Task InitializeAsync();

        event EventHandler<ThemeChangedEventArgs> ThemeChanged;
        // TODO: Create Theme
    }
}
