using Microsoft.Extensions.Logging;
using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.DialogServices.Interfaces;
using WPF_Starter.Services.Export.Interfaces;
using WPF_Starter.Services.FileServices;
using WPF_Starter.Services.Logging;
using WPF_Starter.Services.MessageServices.Interfaces;
using WPF_Starter.Services.SearchServices;

namespace WPF_Starter.Services.Export
{
    public class ExportToXml : IExportToXml
    {
        public event EventHandler? ExportCompleted;
        public event EventHandler? ExportFailed;
        public event EventHandler? InvalidPath;
        public event EventHandler? InvalidConnectionString;

        private readonly FileLogger _fileLogger;
        private readonly PagingSettings _pagingSettings;
        private readonly Paginator _paginator;
        private readonly Search _search;
        private readonly AppDbContext _dataBase;
        private readonly CreateRootElement _createRootElement;
        private readonly ExportSettings _exportSettings;
        private readonly InitializeXmlFile _initializeXmlFile;
        private readonly IFileDialogService _fileDialogServices;
        private readonly IMessageBoxService _messageService;

        public ExportToXml(IFileDialogService fileDialogServices, InitializeXmlFile initializeXmlFile, ExportSettings exportSettings, CreateRootElement createRootElement,
            AppDbContext dataBase, Search search, Paginator paginator, 
            PagingSettings pagingSettings, IMessageBoxService messageBoxService, FileLogger fileLogger)
        {
            _fileDialogServices = fileDialogServices;
            _initializeXmlFile = initializeXmlFile;
            _exportSettings = exportSettings;
            _search = search;
            _dataBase = dataBase;
            _paginator = paginator;
            _createRootElement = createRootElement;
            _pagingSettings = pagingSettings;
            _messageService = messageBoxService;
            _fileLogger = fileLogger;
        }

        /// <summary>
        ///  Manages export state, create a xml file
        ///  initializes and fills the file with data
        /// </summary>
        public async Task Export()
        {
            await _messageService.ShowProgressAsync("Export to XML", "Exporting data, please wait...", async controller =>
            {
                try
                {
                    _exportSettings.IsExporting = true;
                    _exportSettings.XmlFileName = _fileDialogServices.CreateFile("XML Files|*.xml", "Choose Xml file");

                    if (string.IsNullOrEmpty(_exportSettings.XmlFileName))
                    {
                        InvalidPath?.Invoke(this, EventArgs.Empty);
                        return;
                    }

                    _initializeXmlFile.InitializeFile(_exportSettings.XmlFileName);
                    await Task.Run(() => _createRootElement.Fill(_dataBase, _exportSettings, _search, _paginator, _pagingSettings,
                        count => controller.SetMessage($"Exporting data, please wait... \n\nProcessed {count:N0} rows")));
                    ExportCompleted?.Invoke(this, EventArgs.Empty);
                }
                catch (InvalidOperationException ex)
                {
                    _fileLogger.LogError($"{ex}\n");
                    InvalidConnectionString?.Invoke(this, EventArgs.Empty);
                }
                catch (ArgumentException ex)
                {
                    _fileLogger.LogError($"{ex}\n");
                    InvalidConnectionString?.Invoke(this, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    _fileLogger.LogError($"{ex}\n");
                    ExportFailed?.Invoke(this, EventArgs.Empty);
                }
                finally
                {
                    _exportSettings.IsExporting = false;
                }
            }, false);

           
        }
    }
}
