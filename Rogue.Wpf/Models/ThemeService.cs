using Rogue.Wpf.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace Rogue.Wpf.Models
{
    public class ThemeService : IThemeService
    {
        private IThemeReader themeReader { get; }

        private IThemeFileWriter themeFileWriter { get; }

        public readonly string CustomThemesPath = Path.Combine(AppContext.BaseDirectory, "Themes");

        public event EventHandler<ThemeChangedEventArgs> ThemeChanged;

        private Theme activeTheme;

        public Theme ActiveTheme
        {
            get => activeTheme;
            private set
            {
                if (value == activeTheme)
                {
                    return;
                }

                ThemeChanged?.Invoke(this, new ThemeChangedEventArgs(activeTheme, value));
                activeTheme = value;
            }
        }

        public List<Theme> DefaultThemes { get; private set; }

        public List<Theme> CustomThemes { get; private set; }

        public ThemeService(IThemeReader themeReader, IThemeFileWriter themeFileWriter)
        {
            this.themeReader = themeReader;
            this.themeFileWriter = themeFileWriter;
        }

        public async Task InitializeAsync()
        {
            DefaultThemes = new List<Theme>(themeReader.GetAllDefaultThemes());
            SetTheme(Properties.Settings.Default.ActiveTheme ?? DefaultThemes.First());
            CustomThemes = new List<Theme>(await themeReader.GetAllCustomThemesAsync(CustomThemesPath));
        }

        public void SetTheme(Theme theme)
        {
            foreach (var color in theme.ThemeColors)
            {
                try
                {
                    Application.Current.Resources[color.Key] = ColorConverter.ConvertFromString(color.Value);
                    Application.Current.Resources[color.Key + "BrushKey"] = (SolidColorBrush) new BrushConverter().ConvertFrom(color.Value);
                }
                catch (Exception e) when (e is FormatException || e is NotSupportedException)
                {
                    MessageBox.Show("Loading theme failed. Found a non-valid color value in theme.");
                    foreach (var originalThemeColor in this.ActiveTheme.ThemeColors)
                    {
                        Application.Current.Resources[originalThemeColor.Key] = ColorConverter.ConvertFromString(originalThemeColor.Value);
                        Application.Current.Resources[originalThemeColor.Key + "BrushKey"] = (SolidColorBrush) new BrushConverter().ConvertFrom(originalThemeColor.Value);
                    }
                    return;
                }
            }
            this.ActiveTheme = theme;
            Properties.Settings.Default.ActiveTheme = ActiveTheme;
            Properties.Settings.Default.Save();
        }

        public Task CreateTheme()
        {
            throw new NotImplementedException();
        }
    }
}
