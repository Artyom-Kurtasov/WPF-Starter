using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using WPF_Starter.Models;
using WPF_Starter.View;

namespace WPF_Starter.ViewModels.Commands
{
    public class NavigationCommands
    {
        private readonly States _states;
        private Export _export;
        public ICommand ShowExportForm { get; }

        public NavigationCommands(Export export, States states)
        {
            ShowExportForm = new RelayCommands(ShowExportFormExecute, CanShowExportForm);
            _export = export;
            _states = states;
        }

        private void ShowExportFormExecute()
        {
            _export = App.ServiceProvider.GetRequiredService<Export>();
            _export.DataContext = App.ServiceProvider.GetRequiredService<MainWindowViewModel>();
            _export.Show();
        }

        private bool CanShowExportForm() => true;
    }
}
