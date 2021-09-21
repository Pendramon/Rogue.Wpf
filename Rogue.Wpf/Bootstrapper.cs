using System.Linq;
using System.Threading.Tasks;
using Rogue.Wpf.Models.Interfaces;
using Rogue.Wpf.ViewModels.Interfaces;

namespace Rogue.Wpf
{
    public class Bootstrapper
    {
        private readonly MainWindow mainWindow;
        private readonly IMainWindowViewModel mainWindowViewModel;
        private readonly IThemeService themeService;

        public Bootstrapper(MainWindow mainWindow, IMainWindowViewModel mainWindowViewModel, IThemeService themeService)
        {
            this.mainWindow = mainWindow;
            this.mainWindowViewModel = mainWindowViewModel;
            this.themeService = themeService;
        }

        public async Task Run()
        {
            mainWindow.DataContext = mainWindowViewModel;
            mainWindow.Show();
            await this.themeService.InitializeAsync();
            // TODO: Temporary remove later
            this.themeService.SetTheme(this.themeService.CustomThemes.First());
        }
    }
}
