using System.Security.AccessControl;
using System.Windows;
using WPF_Starter.Services.MessageServices.Interfaces;

namespace WPF_Starter.Services.Export
{
    public class ExportNotifyer
    {
        private readonly IMessageBoxService _messageService;
        private readonly ExportToExcel _exportToExcel;
        private readonly ExportToXml _exportToXml;

        public ExportNotifyer(IMessageBoxService messageBoxService, ExportToExcel exportToExcel, ExportToXml exportToXml)
        {
            _exportToExcel = exportToExcel;
            _messageService = messageBoxService;
            _exportToXml = exportToXml;

            _exportToExcel.ExportToExcelCompleted += OnExportCompleted;
            _exportToXml.ExportToXmlCompleted += OnExportCompleted;
        }

        private void OnExportCompleted()
        {
            _messageService.ShowMessage("Export has been completed.",
                "Succsess",
                MessageBoxImage.Information,
                MessageBoxButton.OK);
        }
    }
}
