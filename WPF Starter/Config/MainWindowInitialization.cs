using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WPF_Starter.DataBase;
using WPF_Starter.Models;
using WPF_Starter.ViewModels;

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
            var commands = _serviceProvider.GetRequiredService<Commands>();
            var states = _serviceProvider.GetRequiredService<States>();

            mainWindow.DataContext = new MainWindowViewModel(commands, states);

            return mainWindow;
        }
    }
}
