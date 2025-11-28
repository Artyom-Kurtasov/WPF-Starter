using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WPF_Starter.DataBase;
using WPF_Starter.Models;
using WPF_Starter.ViewModels.ExportData;

namespace WPF_Starter.ViewModels.Commands
{
    public class ExportCommands
    {
        private readonly ExportToXml _exportToXml;
        private readonly ExportToExcel _exportToExcel;
        public ICommand ExportToXmlFile { get; }
        public ICommand ExportToExcelFile { get; }

        public ExportCommands(ExportToXml exportToXml, ExportToExcel exportToExcel)
        {
            ExportToExcelFile = new RelayCommands(ExportExcelExecute, CanExportExcelExecute);
            ExportToXmlFile = new RelayCommands(ExportXmlExecute, CanExportXmlExecute);

            _exportToXml = exportToXml;
            _exportToExcel = exportToExcel;
        }

        private void ExportExcelExecute() => _exportToExcel.Export();

        private void ExportXmlExecute() => _exportToXml.Export();

        private bool CanExportExcelExecute() => true;
        private bool CanExportXmlExecute() => true;
    }
}
