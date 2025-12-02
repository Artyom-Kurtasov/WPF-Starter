using WPF_Starter.Services.Import;
using WPF_Starter.Services.MessageServices.Interfaces;
using System.Windows;
using WPF_Starter.Services.Export;
using WPF_Starter.Config;

namespace WPF_Starter.Services.Notifiers
{
    public class ErrorNotifier
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly ImportCsv _importCsv;
        private readonly ExportToExcel _exportToExcel;
        private readonly ExportToXml _exportToXml;

        public ErrorNotifier(IMessageBoxService messageBoxService, ImportCsv importCsv, ExportToXml exportToXml, ExportToExcel exportToExcel)
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

        private void OnInvalidPath()
        {
            _messageBoxService.ShowMessage("No path has been selected.",
                "Error",
                MessageBoxImage.Error,
                MessageBoxButton.OK);
        }

        public void OnErrorOccurred()
        {
            _messageBoxService.ShowMessage("Something went wrong. Please try again. More information has been saved to the log file.",
                "Error",
                MessageBoxImage.Error,
                MessageBoxButton.OK);
        }

        public void OnFileNotFound()
        {
            _messageBoxService.ShowMessage(
                "File not found. You can import it through the application.",
                "Error",
                MessageBoxImage.Error,
                MessageBoxButton.OK);
        }
    }
}
