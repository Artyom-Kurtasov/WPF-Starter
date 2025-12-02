using WPF_Starter.Models;
using WPF_Starter.Services.DataGridServices;
using WPF_Starter.ViewModels.Commands;

namespace WPF_Starter.ViewModels
{
    public class ExportWindowViewModel
    {
        public PeopleFormState PeopleFormState { get; }
        public ClearCommands ClearCommands { get; }
        public ExportCommands ExportCommands { get; }
        public DataGridManager DataGridManager { get; }
        public ExportSettings ExportSettings { get; }

        public ExportWindowViewModel(
            PeopleFormState peopleFormState,
            ClearCommands clearCommands,
            ExportCommands exportCommands,
            DataGridManager dataGridManager,
            ExportSettings exportSettings)
        {
            PeopleFormState = peopleFormState;
            ClearCommands = clearCommands;
            ExportCommands = exportCommands;
            DataGridManager = dataGridManager;
            ExportSettings = exportSettings;
        }
    }

}
