using ClosedXML.Excel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using WPF_Starter.DataBase;
using WPF_Starter.Models;
using System.Windows.Controls;
using WPF_Starter.View;
using Microsoft.Extensions.DependencyInjection;

namespace WPF_Starter.ViewModels
{
    public class Commands
    {
        private readonly AppDbContext _appDbContext;
        private ExportToExcel _exportToExcel;
        private readonly ToExcel _ToExcel;
        private readonly States _states;
        private readonly Search _search;

        private readonly DataGridManager _gridManager;
        public ICommand ExportToExcelFile { get; }
        public ICommand ShowExportForm { get; }

        public Commands(DataGridManager dataGridManager, ToExcel ToExcel, States states, Search search, AppDbContext appDbContext)
        {
           
            ExportToExcelFile = new RelayCommands(ExportExcelExecute, CanExportExcelExecute);
            ShowExportForm = new RelayCommands(ShowExportFormExecute, CanShowExportForm);

            _appDbContext = appDbContext;
            _gridManager = dataGridManager;
            _ToExcel = ToExcel;
            _states = states;
            _search = search;
        }

        private void ShowExportFormExecute()
        {
            _exportToExcel = App.ServiceProvider.GetRequiredService<ExportToExcel>();
            _exportToExcel.DataContext = new MainWindowViewModel(this, _states);
            _exportToExcel.Show();
        }
        private void ExportExcelExecute()
        {
            _ToExcel.FillExcelFile(_appDbContext);
        }

        private bool CanShowExportForm() => true;
        private bool CanExportExcelExecute() => true;
    }
}
