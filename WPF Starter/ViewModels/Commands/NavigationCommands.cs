using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Input;
using WPF_Starter.Config;
using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.DataGridServices;
using WPF_Starter.Services.SearchServices;
using WPF_Starter.View;

namespace WPF_Starter.ViewModels.Commands
{
    public class NavigationCommands
    {
        private readonly IServiceProvider _services;
        private readonly AppDbContext _appDbContext;
        private readonly Search _search;
        private readonly PagingSettings _pagingSettings;
        private readonly LoadingState _loadingState;
        private readonly DataGridManager _dataGridManager;
        private readonly ExportSettings _exportSettings;
        public ICommand ShowExportForm { get; }
        public ICommand GetNextPage { get; }
        public ICommand GetPreviosPage { get; }
        public ICommand ExitApp { get; }

        public NavigationCommands(LoadingState loadingState, PagingSettings pagingSettings,
            AppDbContext appDbContext, Search search, DataGridManager dataGridManager, ExportSettings exportSettings, IServiceProvider services)
        {
            ShowExportForm = new RelayCommand(ShowExportFormExecute, CanShowExportForm);
            GetNextPage = new RelayCommand(SetNextPage, CanSetNextPage);
            GetPreviosPage = new RelayCommand(SetPreviosPage, CanSetPreviosPage);
            ExitApp = new RelayCommand(Exit, CanExit);

            _loadingState = loadingState;
            _pagingSettings = pagingSettings;
            _appDbContext = appDbContext;
            _search = search;
            _dataGridManager = dataGridManager;
            _exportSettings = exportSettings;
            _services = services;

            _loadingState.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(LoadingState.IsLoading))
                {
                    (GetNextPage as RelayCommand)?.RaiseCanExecuteChanged();
                    (GetPreviosPage as RelayCommand)?.RaiseCanExecuteChanged();
                    (ShowExportForm as RelayCommand)?.RaiseCanExecuteChanged();
                    (ExitApp as RelayCommand)?.RaiseCanExecuteChanged();
                }
            };
            _exportSettings.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(ExportSettings.IsExporting))
                {
                    (GetNextPage as RelayCommand)?.RaiseCanExecuteChanged();
                    (GetPreviosPage as RelayCommand)?.RaiseCanExecuteChanged();
                    (ShowExportForm as RelayCommand)?.RaiseCanExecuteChanged();
                    (ExitApp as RelayCommand)?.RaiseCanExecuteChanged();
                }
            };
        }

        private void Exit() => Application.Current.Shutdown();
        private void SetNextPage()
        {
            _pagingSettings.Page++;
            _pagingSettings.GridPeoples = _dataGridManager.GetPage(_appDbContext, _pagingSettings, _search);
            UpdatePageIndicator();
        }
        private void SetPreviosPage()
        {
            if (_pagingSettings.Page > 0) _pagingSettings.Page--;
            _pagingSettings.GridPeoples = _dataGridManager.GetPage(_appDbContext, _pagingSettings, _search);
            UpdatePageIndicator();
        }
        private void UpdatePageIndicator()
        {
            _pagingSettings.PageIndicator = $"Page {_pagingSettings.Page + 1}";
        }
        private void ShowExportFormExecute()
        {
            ExportWindowInitialization exportInit = _services.GetRequiredService<ExportWindowInitialization>();
            Export? exportWindow = exportInit.Init();
            exportWindow.Show();

        }
        private bool CanShowExportForm() => !_loadingState.IsLoading;
        private bool CanExit() => !_loadingState.IsLoading && !_exportSettings.IsExporting;
        private bool CanSetNextPage() => !_loadingState.IsLoading && !_exportSettings.IsExporting;
        private bool CanSetPreviosPage() => !_loadingState.IsLoading && !_exportSettings.IsExporting;
    }
}
