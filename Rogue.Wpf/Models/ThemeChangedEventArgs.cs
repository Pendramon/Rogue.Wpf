using System;

namespace Rogue.Wpf.Models
{
    public class ThemeChangedEventArgs : EventArgs
    {
        public Theme PreviousTheme { get; }

        public Theme NewTheme { get; }

        public ThemeChangedEventArgs(Theme previousTheme, Theme newTheme)
        {
            PreviousTheme = previousTheme;
            NewTheme = newTheme;
        }
    }
}
