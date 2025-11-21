using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Input;
using WPF_Starter.Config;
using WPF_Starter.View;
using WPF_Starter.ViewModels;

namespace WPF_Starter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }
        private DataGridManager _gridManager;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ServiceCollection services = new();
            var config = new Configure();
            config.conf(services);
            ServiceProvider = services.BuildServiceProvider();

            var csvLoader = ServiceProvider.GetRequiredService<CsvLoader>();
            var dataBaseLoader = ServiceProvider.GetRequiredService<DataBaseLoader>();
            var commands = ServiceProvider.GetRequiredService<Commands>();
            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            MainWindow = mainWindow;
            MainWindow.DataContext = commands;
            _gridManager = ServiceProvider.GetRequiredService<DataGridManager>();

            csvLoader.ChooseFile();
            dataBaseLoader.LoadDataBase();
            _gridManager.SetDataGrid(mainWindow.peoplesGrid);
            MainWindow.Show();
        }
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
            Current.Shutdown();
        }
    }

}
