using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Rogue.Wpf.ViewModels;
using Rogue.Wpf.ViewModels.Interfaces;
using Rogue.Wpf.ViewModels.MainContent;
using Rogue.Wpf.ViewModels.MainContent.Interfaces;
using System.Windows;
using Rogue.Wpf.Models;
using Rogue.Wpf.Models.Interfaces;

namespace Rogue.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private AutofacServiceProvider CompositionRoot()
        {
            // The Microsoft.Extensions.DependencyInjection.ServiceCollection
            // has extension methods provided by other .NET Core libraries to
            // register services with DI.
            // ReSharper disable once CollectionNeverUpdated.Local
            var serviceCollection = new ServiceCollection();

            var containerBuilder = new ContainerBuilder();

            // Once you've registered everything in the ServiceCollection, call
            // Populate to bring those registrations into Autofac. This is
            // just like a foreach over the list of things in the collection
            // to add them to Autofac.
            containerBuilder.Populate(serviceCollection);

            // Make your Autofac registrations. Order is important!
            // If you make them BEFORE you call Populate, then the
            // registrations in the ServiceCollection will override Autofac
            // registrations; if you make them AFTER Populate, the Autofac
            // registrations will override. You can make registrations
            // before or after Populate, however you choose.
            containerBuilder.RegisterType<Bootstrapper>().SingleInstance();
            containerBuilder.RegisterType<MainWindow>();
            containerBuilder.RegisterType<MainWindowViewModel>().As<IMainWindowViewModel>();
            containerBuilder.RegisterType<MainContentViewModel>().As<IMainContentViewModel>();
            containerBuilder.RegisterType<ThemeEditorViewModel>().As<IThemeEditorViewModel>();
            containerBuilder.RegisterType<ThemeReader>().As<IThemeReader>();
            containerBuilder.RegisterType<ThemeFileWriter>().As<IThemeFileWriter>();
            containerBuilder.RegisterType<ThemeService>().As<IThemeService>().SingleInstance();

            // Creating a new AutofacServiceProvider makes the container
            // available to your app using the Microsoft IServiceProvider
            // interface so you can use those abstractions rather than
            // binding directly to Autofac.
            var container = containerBuilder.Build();
            return new AutofacServiceProvider(container);
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var serviceProvider = CompositionRoot();
            var bootstrapper = serviceProvider.GetRequiredService<Bootstrapper>();
            await bootstrapper.Run();
        }
    }
}
