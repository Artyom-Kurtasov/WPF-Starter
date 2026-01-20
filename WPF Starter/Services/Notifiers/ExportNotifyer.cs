using MahApps.Metro.Controls.Dialogs;
using WPF_Starter.Services.Export.Interfaces;
using WPF_Starter.Services.MessageServices.Interfaces;

namespace WPF_Starter.Services.Notifiers
{
    public class ExportNotifyer : IDisposable
    {
        private readonly IMessageBoxService _messageService;
        private readonly IExportToExcel _exportToExcel;
        private readonly IExportToXml _exportToXml;
        private bool _isDisposed = false;

        public ExportNotifyer(IMessageBoxService messageBoxService, IExportToExcel exportToExcel, IExportToXml exportToXml)
        {
            _exportToExcel = exportToExcel;
            _messageService = messageBoxService;
            _exportToXml = exportToXml;

            _exportToExcel.ExportCompleted += OnExportCompleted;
            _exportToXml.ExportCompleted += OnExportCompleted;
        }

        private void OnExportCompleted(object? sender, EventArgs e)
        {
            _exportToExcel.ExportCompleted -= OnExportCompleted;
            _exportToXml.ExportCompleted -= OnExportCompleted;

            _messageService.ShowMessageAsync("Success",
                "Export has been completed.",
                MessageDialogStyle.Affirmative);
        }

        public void Dispose()
        {
            if (_isDisposed) return;

            _isDisposed = true;

            _exportToExcel.ExportCompleted -= OnExportCompleted;
            _exportToXml.ExportCompleted -= OnExportCompleted;
        }
    }
}
