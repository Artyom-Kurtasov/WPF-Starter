using System.Security.AccessControl;
using System.Windows;
using WPF_Starter.Services.Export;
using WPF_Starter.Services.MessageServices.Interfaces;

namespace WPF_Starter.Services.Notifiers
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

            _exportToExcel.ExportCompleted += OnExportCompleted;
            _exportToXml.ExportCompleted += OnExportCompleted;
        }

        private void OnExportCompleted()
        {
            _messageService.ShowMessage("Export has been completed.",
                "Success",
                MessageBoxImage.Information,
                MessageBoxButton.OK);
        }
    }
}
