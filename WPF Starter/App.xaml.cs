using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Input;
using WPF_Starter.Config;
using WPF_Starter.Models;
using WPF_Starter.View;
using WPF_Starter.ViewModels;

namespace WPF_Starter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider? ServiceProvider { get; private set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var culture = new System.Globalization.CultureInfo("ru-RU");
            System.Threading.Thread.CurrentThread.CurrentCulture = culture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = culture;

            ServiceCollection services = new();
            var config = new Configure();
            config.conf(services);
            ServiceProvider = services.BuildServiceProvider();

            var mainWindowInitialization = ServiceProvider.GetRequiredService<MainWindowInitialization>();
            var mainWindow = mainWindowInitialization.Init();

            var dataLoader = ServiceProvider.GetRequiredService<StartupDataLoader>();
            dataLoader.InitializationData(mainWindow);

            MainWindow = mainWindow;
            MainWindow.Show();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Current.Shutdown();
        }
    }

}
