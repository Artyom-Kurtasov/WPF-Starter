using WPF_Starter.Models;
using WPF_Starter.ViewModels.Commands;

namespace WPF_Starter.ViewModels
{
    public class MainWindowViewModel
    {
        public NavigationCommands NavigationCommands { get; }
        public PagingSettings PagingSettings { get; }
        public LoadingState LoadingState { get; }
        public ImportCommands ImportCommands { get; }

        public MainWindowViewModel(NavigationCommands navigationCommands,
                                   PagingSettings pagingSettings,
                                   LoadingState loadingState,
                                   ImportCommands importCommands)
        {
            NavigationCommands = navigationCommands;
            PagingSettings = pagingSettings;
            LoadingState = loadingState;
            ImportCommands = importCommands;
        }
    }

}
