using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Windows;
using WPF_Starter.Config;
using WPF_Starter.Config.Initialization;
using WPF_Starter.Services.MessageServices.Interfaces;

namespace WPF_Starter
{
    public partial class App : Application
    {
        public static ServiceProvider? ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureCulture();
            ConfigureServices();
            InitializeMainWindow();
        }

        private void ConfigureCulture()
        {
            var culture = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        private void ConfigureServices()
        {
            var services = new ServiceCollection();
            var configure = new Configure();
            configure.ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        private void InitializeMainWindow()
        {
            var mainWindowInitialization = ServiceProvider!.GetRequiredService<MainWindowInitialization>();
            var mainWindow = mainWindowInitialization.InitMainWindow();
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Current.Shutdown();
        }
    }
}