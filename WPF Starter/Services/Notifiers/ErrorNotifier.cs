using WPF_Starter.Services.MessageServices.Interfaces;
using WPF_Starter.Services.Import.Interfaces;
using WPF_Starter.Services.Export.Interfaces;
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



            _importCsv.ImportCsvFailed += OnUnexpectedErrorOccurred;
            _exportToExcel.ExportFailed += OnUnexpectedErrorOccurred;
            _exportToXml.ExportFailed += OnUnexpectedErrorOccurred;
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
            await _messageBoxService.ShowMessageAsync(
                                "Error",
                "Csv file not found. You can import it through the application.",
                MessageDialogStyle.Affirmative);
        }
    }
}
