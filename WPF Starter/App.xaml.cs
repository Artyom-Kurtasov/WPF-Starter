using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WPF_Starter.Config;
using WPF_Starter.Models;
using WPF_Starter.Services;

namespace WPF_Starter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider? ServiceProvider { get; private set; }
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var culture = new System.Globalization.CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            ServiceCollection services = new();
            var config = new Configure();
            config.conf(services);
            ServiceProvider = services.BuildServiceProvider();

            var subscriptionManager = ServiceProvider.GetRequiredService<SubscriptionManager>();

            var loading = ServiceProvider.GetRequiredService<LoadingState>();
            var mainWindowInitialization = ServiceProvider.GetRequiredService<MainWindowInitialization>();
            var mainWindow = mainWindowInitialization.Init();

            MainWindow = mainWindow;
            MainWindow.Show();

            loading.IsLoading = true;
            var dataLoader = ServiceProvider.GetRequiredService<StartupDataLoader>();
            await dataLoader.InitializeAsync();
            loading.IsLoading = false;
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Current.Shutdown();
        }
    }

}
