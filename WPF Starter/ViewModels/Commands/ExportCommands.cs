using System.Windows.Input;
using WPF_Starter.Models;
using WPF_Starter.Services.Export.Interfaces;

namespace WPF_Starter.ViewModels.Commands
{
    public class ExportCommands
    {
        private readonly IExportToXml _exportToXml;
        private readonly IExportToExcel _exportToExcel;
        public ICommand ExportToXmlFile { get; }
        public ICommand ExportToExcelFile { get; }

        public ExportCommands(IExportToXml exportToXml, IExportToExcel exportToExcel)
        {
            ExportToExcelFile = new AsyncRelayCommand(ExportExcelAsync, CanExportExcelExecute);
            ExportToXmlFile = new AsyncRelayCommand(ExportXmlAsync, CanExportXmlExecute);

            _exportToXml = exportToXml;
            _exportToExcel = exportToExcel;
        }
        private async Task ExportExcelAsync() => await _exportToExcel.Export();

        private async Task ExportXmlAsync() => await _exportToXml.Export();

        private bool CanExportExcelExecute() => true;
        private bool CanExportXmlExecute() => true;
    }
}
