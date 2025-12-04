using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.IO;
using System.Windows;
using WPF_Starter.Config;
using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.Notifiers;

namespace WPF_Starter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider? ServiceProvider { get; private set; }

        public event EventHandler? FileNotFound;
        public event EventHandler? LoadFailed;
        public event EventHandler? LoadCompletedEvent;

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ConfigureCulture();
            ConfigureServices();

            InitializeMainWindow();

            SubscribeNotifiers();

            await LoadStartupDataAsync();
        }

        private void ConfigureCulture()
        {
            CultureInfo? culture = new CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        private void ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            Configure? configure = new Configure();
            configure.ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();
        }

        private void InitializeMainWindow()
        {
            MainWindowInitialization mainWindowInitialization = ServiceProvider!.GetRequiredService<MainWindowInitialization>();
            MainWindow = mainWindowInitialization.InitMainWindow();
            MainWindow.Show();
        }

        private void SubscribeNotifiers()
        {
            ErrorNotifier? errorNotifier = ServiceProvider!.GetRequiredService<ErrorNotifier>();
            DataBaseNotifier? dataBaseNotifier = ServiceProvider!.GetRequiredService<DataBaseNotifier>();

            FileNotFound += errorNotifier.OnFileNotFound;
            LoadFailed += errorNotifier.OnErrorOccurred;
            LoadCompletedEvent += dataBaseNotifier.OnLoadCompleted;
        }

        private async Task LoadStartupDataAsync()
        {
            LoadingState? loading = ServiceProvider!.GetRequiredService<LoadingState>();
            AppDbContext? appDbContext = ServiceProvider!.GetRequiredService<AppDbContext>();
            ExportSettings? filePath = ServiceProvider!.GetRequiredService<ExportSettings>();
            IStartupDataLoader? dataLoader = ServiceProvider!.GetRequiredService<IStartupDataLoader>();

            try
            {
                await dataLoader.InitializeAsync(filePath.CsvFilePath, appDbContext);
                LoadCompletedEvent?.Invoke(this, EventArgs.Empty);
            }
            catch (FileNotFoundException)
            {
                FileNotFound?.Invoke(this, EventArgs.Empty);
            }
            catch (Exception)
            {
                LoadFailed?.Invoke(this, EventArgs.Empty);
            }
            finally
            {
                loading.IsLoading = false;
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Current.Shutdown();
        }
    }

}
