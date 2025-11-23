using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using WPF_Starter.DataBase;
using WPF_Starter.ViewModels;

namespace WPF_Starter.Config
{
    public class StartupDataLoader
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AppDbContext _appDbContext;

        public StartupDataLoader(IServiceProvider serviceProvider, AppDbContext appDbContext)
        {
            _serviceProvider = serviceProvider;
            _appDbContext = appDbContext;
        }

        public void InitializationData(MainWindow mainWindow)
        {
            var csvLoader = _serviceProvider.GetRequiredService<CsvLoader>();
            var dataBaseLoader = _serviceProvider.GetRequiredService<DataBaseLoader>();
            var gridManager = _serviceProvider.GetRequiredService<DataGridManager>();

            csvLoader.ChooseFile();
            dataBaseLoader.LoadDataBase(_appDbContext);
            gridManager.SetDataGrid(mainWindow.peoplesGrid, _appDbContext);
        }
    }
}
