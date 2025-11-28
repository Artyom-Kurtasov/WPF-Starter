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
        public PeopleFormState PeopleFormState { get; }
        public MainWindowViewModel(PeopleFormState peopleFormState, ClearCommands clearCommands, ExportCommands exportCommands, NavigationCommands navigationCommands)
        {
            ClearCommands = clearCommands;
            ExportCommands = exportCommands;
            NavigationCommands = navigationCommands;
            PeopleFormState = peopleFormState;
        }
    }
}
