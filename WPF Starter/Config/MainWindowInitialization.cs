using Microsoft.Extensions.DependencyInjection;
using WPF_Starter.Models;
using WPF_Starter.Services.Notifiers;
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
            MainWindow? mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            NavigationCommands? navigationCommands = _serviceProvider.GetRequiredService<NavigationCommands>();
            PagingSettings? pagingSettings = _serviceProvider.GetRequiredService<PagingSettings>();
            LoadingState? loadingState = _serviceProvider.GetRequiredService<LoadingState>();
            ImportCommands? importCommands = _serviceProvider.GetRequiredService<ImportCommands>();

            DataBaseNotifier? startupNotifier = _serviceProvider.GetRequiredService<DataBaseNotifier>();
            ImportNotifier? importNotifier = _serviceProvider.GetRequiredService<ImportNotifier>();

            mainWindow.DataContext = new MainWindowViewModel(
                navigationCommands,
                pagingSettings,
                loadingState,
                importCommands);

            return mainWindow;
        }

    }
}
