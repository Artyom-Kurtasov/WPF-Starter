using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WPF_Starter.DataBase;
using WPF_Starter.Models;
using WPF_Starter.ViewModels.Commands;

namespace WPF_Starter.ViewModels
{
    public class MainWindowViewModel
    {
        public ClearCommands ClearCommands { get; }
        public ExportCommands ExportCommands { get; }
        public NavigationCommands NavigationCommands { get; }
        public States States { get; }
        public MainWindowViewModel(States states, ClearCommands clearCommands, ExportCommands exportCommands, NavigationCommands navigationCommands)
        {
            ClearCommands = clearCommands;
            ExportCommands = exportCommands;
            NavigationCommands = navigationCommands;
            States = states;
        }
    }
}
