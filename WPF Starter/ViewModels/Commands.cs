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
        private readonly ClearFieldsExportForm _clearFieldsExportForm;
        private readonly AppDbContext _appDbContext;
        private ExportToExcel _exportToExcel;
        private readonly ToXml _toXml;
        private readonly ToExcel _toExcel;
        private readonly States _states;
        private readonly Search _search;

        private readonly DataGridManager _gridManager;
        public ICommand ExportToExcelFile { get; }
        public ICommand ShowExportForm { get; }
        public ICommand ExportToXmlFile { get; }

        public Commands(DataGridManager dataGridManager, ToExcel ToExcel, States states, Search search, AppDbContext appDbContext, ClearFieldsExportForm clearFields,
            ExportToExcel exportToExcel, ToXml toXml)
        {
           
            ExportToExcelFile = new RelayCommands(ExportExcelExecute, CanExportExcelExecute);
            ShowExportForm = new RelayCommands(ShowExportFormExecute, CanShowExportForm);
            ExportToXmlFile = new RelayCommands(ExportXmlExecute, CanExportXmlExecute);

            _exportToExcel = exportToExcel;
            _appDbContext = appDbContext;
            _gridManager = dataGridManager;
            _toExcel = ToExcel;
            _states = states;
            _search = search;
            _clearFieldsExportForm = clearFields;
            _toXml = toXml;
        }

        private void ExportXmlExecute()
        {
            _toXml.FillXmlFile(_appDbContext);
            _clearFieldsExportForm.ClearFields();
        }
        private void ShowExportFormExecute()
        {
            _exportToExcel = App.ServiceProvider.GetRequiredService<ExportToExcel>();
            _exportToExcel.DataContext = new MainWindowViewModel(this, _states);
            _exportToExcel.Show();
        }
        private void ExportExcelExecute()
        {
            _toExcel.FillExcelFile(_appDbContext);
            _clearFieldsExportForm.ClearFields();
        }

        private bool CanExportXmlExecute() => true;
        private bool CanShowExportForm() => true;
        private bool CanExportExcelExecute() => true;
    }
}
