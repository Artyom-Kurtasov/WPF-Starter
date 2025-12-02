using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Windows;
using WPF_Starter.Config;
using WPF_Starter.Models;
using WPF_Starter.Services;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.MessageServices;
using WPF_Starter.Services.Notifiers;

namespace WPF_Starter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider? ServiceProvider { get; private set; }
        public event Action? FileNotFound;
        public event Action? LoadFailed;
        public event Action? LoadCompletedEvent;
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var culture = new System.Globalization.CultureInfo("ru-RU");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;

            var services = new ServiceCollection();
            var configure = new Configure();
            configure.conf(services);

            ServiceProvider = services.BuildServiceProvider();

            var loading = ServiceProvider.GetRequiredService<LoadingState>();
            var errorNotifier = ServiceProvider.GetRequiredService<ErrorNotifier>();
            var dataBaseNotifier = ServiceProvider.GetService<DataBaseNotifier>();
            var mainWindowInitialization = ServiceProvider.GetRequiredService<MainWindowInitialization>();
            var appDbContext = ServiceProvider.GetRequiredService<AppDbContext>();
            var filePath = ServiceProvider.GetRequiredService<ExportSettings>();

            MainWindow = mainWindowInitialization.InitMainWindow();
            MainWindow.Show();


            this.FileNotFound += errorNotifier.OnFileNotFound;
            this.LoadFailed += errorNotifier.OnErrorOccurred;
            this.LoadCompletedEvent += dataBaseNotifier.OnLoadCompleted;

            var dataLoader = ServiceProvider.GetRequiredService<StartupDataLoader>();
            try
            {
                await dataLoader.InitializeAsync(filePath.CsvFilePath, appDbContext);
                LoadCompletedEvent?.Invoke();
            }
            catch (FileNotFoundException)
            {
                FileNotFound?.Invoke();
            }
            catch (Exception)
            {
                LoadFailed?.Invoke();
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
