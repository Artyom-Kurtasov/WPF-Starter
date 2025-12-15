using WPF_Starter.Services.MessageServices.Interfaces;
using System.Windows;
using WPF_Starter.Services.Import.Interfaces;
using WPF_Starter.Services.Export.Interfaces;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace WPF_Starter.Services.Notifiers
{
    public class ErrorNotifier
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly IImportCsv _importCsv;
        private readonly IExportToExcel _exportToExcel;
        private readonly IExportToXml _exportToXml;

        public ErrorNotifier(IMessageBoxService messageBoxService, IImportCsv importCsv, IExportToXml exportToXml, IExportToExcel exportToExcel)
        {
            _messageBoxService = messageBoxService;
            _importCsv = importCsv;
            _exportToXml = exportToXml;
            _exportToExcel = exportToExcel;

            _importCsv.InvalidPath += OnInvalidPath;
            _exportToXml.InvalidPath += OnInvalidPath;
            _exportToExcel.InvalidPath += OnInvalidPath;


            _importCsv.ImportCsvFailed += OnErrorOccurred;
            _exportToExcel.ExportFailed += OnErrorOccurred;
            _exportToXml.ExportFailed += OnErrorOccurred;
        }

        private void OnInvalidPath(object? sender, EventArgs e)
        {
            _messageBoxService.ShowMessage("Error",
                "No path has been selected.",
                MessageDialogStyle.Affirmative);
        }

        public void OnErrorOccurred(object? sender, EventArgs e)
        {
            _messageBoxService.ShowMessage("Error",
                "Something went wrong. Please try again.",
                MessageDialogStyle.Affirmative);
        }

        public void OnFileNotFound(object? sender, EventArgs e)
        {
            _messageBoxService.ShowMessage(
                                "Error",
                "File not found. You can import it through the application.",
                MessageDialogStyle.Affirmative);
        }
    }
}
