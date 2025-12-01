using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.DialogServices.Interfaces;
using WPF_Starter.Services.FileServices;
using WPF_Starter.Services.SearchServices;

namespace WPF_Starter.Services.Export
{
    public class ExportToXml
    {
        public event Action? ExportToXmlCompleted;
        private readonly PagingSettings _pagingSettings;
        private readonly Paginator _paginator;
        private readonly Search _search;
        private readonly AppDbContext _dataBase;
        private readonly CreateRootElement _createRootElement;
        private readonly ExportSettings _exportSettings;
        private readonly InitializeXmlFile _initializeXmlFile;
        private readonly IFileDialogService _fileDialogServices;
        private readonly ErrorNotifier _errorNotifier;

        public ExportToXml(IFileDialogService fileDialogServices, InitializeXmlFile initializeXmlFile, ExportSettings exportSettings, CreateRootElement createRootElement,
            AppDbContext dataBase, Search search, Paginator paginator, PagingSettings pagingSettings, ErrorNotifier errorNotifier)
        {
            _fileDialogServices = fileDialogServices;
            _initializeXmlFile = initializeXmlFile;
            _exportSettings = exportSettings;
            _search = search;
            _dataBase = dataBase;
            _paginator = paginator;
            _createRootElement = createRootElement;
            _pagingSettings = pagingSettings;
            _errorNotifier = errorNotifier;
        }
        public async Task Export()
        {
            _exportSettings.IsExporting = true;
            try
            {
                _exportSettings.XmlFileName = _fileDialogServices.CreateFile("XML Files|*.xml", "Choose Xml file");

                if(string.IsNullOrEmpty(_exportSettings.XmlFileName))
                {
                    _errorNotifier.Notify("No path has been selected to save the file.");
                    return;
                }

                _initializeXmlFile.InitializeFile(_exportSettings.XmlFileName);
               await Task.Run(() => _createRootElement.Fill(_dataBase, _exportSettings, _search, _paginator, _pagingSettings));
                ExportToXmlCompleted?.Invoke();
            }
            catch (Exception)
            {
                _errorNotifier.Notify($"Something went wrong. Please try again. More information has been saved to the log file.");
            }
            finally
            {
                _exportSettings.IsExporting = false;
            }
        }
    }
}
