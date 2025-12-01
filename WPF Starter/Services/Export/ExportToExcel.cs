using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.DialogServices.Interfaces;
using WPF_Starter.Services.FileServices;
using WPF_Starter.Services.SearchServices;

namespace WPF_Starter.Services.Export
{
    public class ExportToExcel
    {
        public event Action? ExportToExcelCompleted;
        private readonly PagingSettings _pagingSettings;
        private readonly Paginator _paginator;
        private readonly Search _search;
        private readonly AppDbContext _dataBase;
        private readonly FillWorksheet _worksheet;
        private readonly ExportSettings _exportSettings;
        private readonly InitializeExcelFile _initializeExcelFile;
        private readonly IFileDialogService _fileDialogServices;
        private readonly ErrorNotifier _errorNotifier;

        public ExportToExcel(IFileDialogService fileDialogServices, InitializeExcelFile initializeExcelFile, ExportSettings exportSettings,
            FillWorksheet worksheet, AppDbContext dataBase, Search search, Paginator paginator, PagingSettings pagingSettings, ErrorNotifier errorNotifier)
        {
            _fileDialogServices = fileDialogServices;
            _initializeExcelFile = initializeExcelFile;
            _exportSettings = exportSettings;
            _worksheet = worksheet;
            _search = search;
            _dataBase = dataBase;
            _paginator = paginator;
            _pagingSettings = pagingSettings;
            _errorNotifier = errorNotifier;
        }
        public async Task Export()
        {
            _exportSettings.IsExporting = true;
            try
            {
                _exportSettings.ExcelFileName = _fileDialogServices.CreateFile("Excel Files|*.xlsx", "Choose Excel file");

                if (string.IsNullOrEmpty(_exportSettings.ExcelFileName))
                {
                    _errorNotifier.Notify("No path has been selected to save the file.");
                    return;
                }

                _initializeExcelFile.InitializeFile(_exportSettings.ExcelFileName);
                await Task.Run(() => _worksheet.Fill(_dataBase, _exportSettings, _search, _paginator, _pagingSettings));
                ExportToExcelCompleted?.Invoke();
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
