using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.DataGridServices;
using WPF_Starter.Services.SearchServices;
using WPF_Starter.ViewModels.Commands;

namespace WPF_Starter.ViewModels
{
    public class MainWindowViewModel
    {
        public DataGridManager DataGridManager { get; }
        public ClearCommands ClearCommands { get; }
        public ExportCommands ExportCommands { get; }
        public NavigationCommands NavigationCommands { get; }
        public PeopleFormState PeopleFormState { get; }
        public PagingSettings PagingSettings { get; }
        public LoadingState LoadingState { get; }


        public MainWindowViewModel(PeopleFormState peopleFormState, ClearCommands clearCommands, ExportCommands exportCommands, 
            NavigationCommands navigationCommands, DataGridManager dataGridManager, AppDbContext dataBase, PagingSettings pagingSettings, Search search,
            LoadingState loadingState)
        {
            ClearCommands = clearCommands;
            ExportCommands = exportCommands;
            NavigationCommands = navigationCommands;
            PeopleFormState = peopleFormState;
            DataGridManager = dataGridManager;
            PagingSettings = pagingSettings;
            LoadingState = loadingState;
        }

    }
}
