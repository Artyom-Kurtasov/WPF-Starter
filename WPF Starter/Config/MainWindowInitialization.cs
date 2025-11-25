using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WPF_Starter.DataBase;
using WPF_Starter.Models;
using WPF_Starter.ViewModels;
using WPF_Starter.ViewModels.Commands;

namespace WPF_Starter.Config
{
    public class MainWindowInitialization
    {
        private readonly IServiceProvider _serviceProvider;

        public MainWindowInitialization(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public MainWindow Init() 
        {
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            var clearCommands = _serviceProvider.GetRequiredService<ClearCommands>();
            var exportCommands = _serviceProvider.GetRequiredService<ExportCommands>();
            var navigationCommands = _serviceProvider.GetRequiredService<NavigationCommands>();
            var states = _serviceProvider.GetRequiredService<States>();

            mainWindow.DataContext = new MainWindowViewModel(states, clearCommands, exportCommands, navigationCommands);

            return mainWindow;
        }
    }
}
