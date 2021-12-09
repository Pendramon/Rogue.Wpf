using Rogue.Wpf.Models.Interfaces;
using Rogue.Wpf.ViewModels.Interfaces;
using System.Threading.Tasks;

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
        }
    }
}
