using Microsoft.Extensions.DependencyInjection;
using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.DataGridServices;
using WPF_Starter.Services.SearchServices;
using WPF_Starter.ViewModels;
using WPF_Starter.ViewModels.Commands;

namespace WPF_Starter.Config
{
    public class MainWindowInitialization
    {
        private readonly StartUpNotifier _startUpNotifyer;
        private readonly IServiceProvider _serviceProvider;

        public MainWindowInitialization(IServiceProvider serviceProvider, StartUpNotifier startUpNotifyer)
        {
            _serviceProvider = serviceProvider;
            _startUpNotifyer = startUpNotifyer;
        }

        public MainWindow Init() 
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            var clearCommands = _serviceProvider.GetRequiredService<ClearCommands>();
            var exportCommands = _serviceProvider.GetRequiredService<ExportCommands>();
            var navigationCommands = _serviceProvider.GetRequiredService<NavigationCommands>();
            var peopleFormState = _serviceProvider.GetRequiredService<PeopleFormState>();
            var dataGrid = _serviceProvider.GetRequiredService<DataGridManager>();
            var dataBase = _serviceProvider.GetRequiredService<AppDbContext>();
            var search = _serviceProvider.GetRequiredService<Search>();
            var pagingSettings = _serviceProvider.GetRequiredService<PagingSettings>();
            var loadingState = _serviceProvider.GetRequiredService<LoadingState>();

            mainWindow.DataContext = new MainWindowViewModel(peopleFormState, clearCommands, exportCommands, navigationCommands, dataGrid,
                dataBase, pagingSettings, search, loadingState);

            return mainWindow;
        }
    }
}
