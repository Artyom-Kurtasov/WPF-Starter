using MahApps.Metro.Controls.Dialogs;
using System.Diagnostics;
using WPF_Starter.Services.Export.Interfaces;
using WPF_Starter.Services.Import.Interfaces;
using WPF_Starter.Services.MessageServices.Interfaces;

namespace WPF_Starter.Services.Notifiers
{
    public class ErrorNotifier : IDisposable
    {
        private readonly IMessageBoxService _messageBoxService;
        private readonly IImportCsv _importCsv;
        private readonly IExportToExcel _exportToExcel;
        private readonly IExportToXml _exportToXml;
        private bool _isDisposed = false;
        public ErrorNotifier(IMessageBoxService messageBoxService, IImportCsv importCsv, IExportToXml exportToXml, IExportToExcel exportToExcel)
        {
            _messageBoxService = messageBoxService;
            _importCsv = importCsv;
            _exportToXml = exportToXml;
            _exportToExcel = exportToExcel;

            _exportToXml.ExportFailed += OnUnexpectedErrorOccurred;
            _exportToXml.InvalidPath += OnInvalidPath;
            _exportToXml.InvalidConnectionString += OnInvalidConnectionStringAsync;

            _exportToExcel.ExportFailed += OnUnexpectedErrorOccurred;
            _exportToExcel.InvalidConnectionString += OnInvalidConnectionStringAsync;
            _exportToExcel.InvalidPath += OnInvalidPath;

            _importCsv.InvalidConnectionString += OnInvalidConnectionStringAsync;
            _importCsv.ImportCsvFailed += OnUnexpectedErrorOccurred;
            _importCsv.InvalidPath += OnInvalidPath;

        }

        private void OnInvalidPath(object? sender, EventArgs e)
        {
            _messageBoxService.ShowMessageAsync("Error",
                "No path has been selected.",
                MessageDialogStyle.Affirmative);
        }

        public void OnUnexpectedErrorOccurred(object? sender, EventArgs e)
        {
            _messageBoxService.ShowMessageAsync("Error",
                "Something went wrong. More information in log file",
                MessageDialogStyle.Affirmative);
        }

        public async void OnFileNotFoundAsync(object? sender, EventArgs e)
        {
            await _messageBoxService.ShowMessageAsync("Error",
                "Csv file not found. You can import it through the application.",
                MessageDialogStyle.Affirmative);
        }

        public async void OnInvalidConnectionStringAsync(object? sender, EventArgs e)
        {
            await _messageBoxService.ShowMessageAsync("Error",
                "Connection string is invalid. Change it in settings.",
                MessageDialogStyle.Affirmative);
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            _isDisposed = true;

            _exportToXml.ExportFailed -= OnUnexpectedErrorOccurred;
            _exportToXml.InvalidPath -= OnInvalidPath;
            _exportToXml.InvalidConnectionString -= OnInvalidConnectionStringAsync;

            _exportToExcel.ExportFailed -= OnUnexpectedErrorOccurred;
            _exportToExcel.InvalidConnectionString -= OnInvalidConnectionStringAsync;
            _exportToExcel.InvalidPath -= OnInvalidPath;

            _importCsv.InvalidConnectionString -= OnInvalidConnectionStringAsync;
            _importCsv.ImportCsvFailed -= OnUnexpectedErrorOccurred;
            _importCsv.InvalidPath -= OnInvalidPath;
        }
    }
}
