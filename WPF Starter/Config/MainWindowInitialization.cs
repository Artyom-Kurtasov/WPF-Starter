using Microsoft.Extensions.DependencyInjection;
using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.DataGridServices;
using WPF_Starter.Services.Notifiers;
using WPF_Starter.Services.SearchServices;
using WPF_Starter.ViewModels;
using WPF_Starter.ViewModels.Commands;

namespace WPF_Starter.Config
{
    public class MainWindowInitialization
    {
        private readonly IServiceProvider _serviceProvider;

        public MainWindowInitialization(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public MainWindow InitMainWindow()
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            var navigationCommands = _serviceProvider.GetRequiredService<NavigationCommands>();
            var pagingSettings = _serviceProvider.GetRequiredService<PagingSettings>();
            var loadingState = _serviceProvider.GetRequiredService<LoadingState>();
            var importCommands = _serviceProvider.GetRequiredService<ImportCommands>();

            var startupNotifier = _serviceProvider.GetRequiredService<DataBaseNotifier>();
            var importNotifier = _serviceProvider.GetRequiredService<ImportNotifier>();

            mainWindow.DataContext = new MainWindowViewModel(
                navigationCommands,
                pagingSettings,
                loadingState,
                importCommands);

            return mainWindow;
        }

    }
}
