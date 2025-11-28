using DocumentFormat.OpenXml.Drawing.Diagrams;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using WPF_Starter.DataBase;
using WPF_Starter.Models;
using WPF_Starter.ViewModels;
using WPF_Starter.ViewModels.DataBaseServices;
using WPF_Starter.ViewModels.FileServices;
using WPF_Starter.ViewModels.SearchServices;

namespace WPF_Starter.Config
{
    public class StartupDataLoader
    {
        private readonly Search _search;
        private readonly Paginator _paginator;
        private readonly PagingSettings _pagingSettings;
        private readonly ExportSettings _exportSettings;
        private readonly IServiceProvider _serviceProvider;
        private readonly AppDbContext _appDbContext;

        public StartupDataLoader(IServiceProvider serviceProvider, AppDbContext appDbContext, ExportSettings exportSettings,
            Paginator paginator, PagingSettings pagingSettings, Search search)
        {
            _serviceProvider = serviceProvider;
            _appDbContext = appDbContext;
            _exportSettings = exportSettings;
            _paginator = paginator;
            _pagingSettings = pagingSettings;
            _search = search;
        }

        public void InitializationData(MainWindow mainWindow)
        {
            var csvLoader = _serviceProvider.GetRequiredService<FilesLoader>();
            var dataBaseLoader = _serviceProvider.GetRequiredService<DataBaseLoader>();
            var gridManager = _serviceProvider.GetRequiredService<DataGridManager>();

            dataBaseLoader.LoadDataBase(_appDbContext);
            gridManager.SetDataGrid(mainWindow.peoplesGrid, _appDbContext, _pagingSettings, _search);
        }
    }
}
