using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
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
            var exportWindow = _serviceProvider.GetRequiredService<Export>();

            var peopleFormState = _serviceProvider.GetRequiredService<PeopleFormState>();
            var clearCommands = _serviceProvider.GetRequiredService<ClearCommands>();
            var exportCommands = _serviceProvider.GetRequiredService<ExportCommands>();
            var dataGridManager = _serviceProvider.GetRequiredService<DataGridManager>();
            var exportSettings = _serviceProvider.GetRequiredService<ExportSettings>();

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
