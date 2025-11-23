using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using WPF_Starter.Models;

namespace WPF_Starter.ViewModels
{
    public class MainWindowViewModel
    {
        public Commands Commands { get; }
        public States States { get; }
        public MainWindowViewModel(Commands commands, States states)
        {
            Commands = commands;
            States = states;
        }
    }
}
