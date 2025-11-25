using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WPF_Starter.DataBase;

namespace WPF_Starter.ViewModels.Commands
{
    public class ExportCommands
    {
        private readonly AppDbContext _appDbContext;
        private readonly ExportToExcel _exportToExcel;
        private readonly ExportToXml _exportToXml;
        public ICommand ExportToXmlFile { get; }
        public ICommand ExportToExcelFile { get; }

        public ExportCommands(ExportToExcel exportToExcel, ExportToXml exportToXml, AppDbContext appDbContext)
        {
            ExportToExcelFile = new RelayCommands(ExportExcelExecute, CanExportExcelExecute);
            ExportToXmlFile = new RelayCommands(ExportXmlExecute, CanExportXmlExecute);

            _exportToExcel = exportToExcel;
            _exportToXml = exportToXml;
            _appDbContext = appDbContext;
        }

        private void ExportExcelExecute() => _exportToExcel.FillExcelFile(_appDbContext);
        private void ExportXmlExecute() => _exportToXml.FillXmlFile(_appDbContext);
        private bool CanExportExcelExecute() => true;
        private bool CanExportXmlExecute() => true;
    }
}
