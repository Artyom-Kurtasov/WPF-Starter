using Microsoft.Extensions.DependencyInjection;
using WPF_Starter.Models;
using WPF_Starter.Services.DataGridServices;
using WPF_Starter.View;
using WPF_Starter.ViewModels;
using WPF_Starter.ViewModels.Commands;

namespace WPF_Starter.Config
{
    public class ExportWindowInitialization
    {
        private readonly IServiceProvider _serviceProvider;

        public ExportWindowInitialization(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Export Init()
        {
            Export? exportWindow = _serviceProvider.GetRequiredService<Export>();

            PeopleFormState? peopleFormState = _serviceProvider.GetRequiredService<PeopleFormState>();
            ClearCommands? clearCommands = _serviceProvider.GetRequiredService<ClearCommands>();
            ExportCommands? exportCommands = _serviceProvider.GetRequiredService<ExportCommands>();
            DataGridManager? dataGridManager = _serviceProvider.GetRequiredService<DataGridManager>();
            ExportSettings? exportSettings = _serviceProvider.GetRequiredService<ExportSettings>();

            exportWindow.DataContext = new ExportWindowViewModel(
                peopleFormState,
                clearCommands,
                exportCommands,
                dataGridManager,
                exportSettings);

            return exportWindow;
        }
    }

}
