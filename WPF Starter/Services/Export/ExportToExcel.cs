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
    public class ExportToExcel : IExportToExcel
    {
        public event EventHandler? ExportCompleted;
        public event EventHandler? ExportFailed;
        public event EventHandler? InvalidPath;
        public event EventHandler? InvalidConnectionString;

        private readonly FileLogger _fileLogger;
        private readonly IMessageBoxService _messageBoxService;
        private readonly PagingSettings _pagingSettings;
        private readonly Paginator _paginator;
        private readonly Search _search;
        private readonly AppDbContext _dataBase;
        private readonly FillWorksheet _worksheet;
        private readonly ExportSettings _exportSettings;
        private readonly InitializeExcelFile _initializeExcelFile;
        private readonly IFileDialogService _fileDialogServices;

        public ExportToExcel(
            IFileDialogService fileDialogServices,
            InitializeExcelFile initializeExcelFile,
            ExportSettings exportSettings,
            FillWorksheet worksheet,
            AppDbContext dataBase,
            Search search,
            Paginator paginator,
            PagingSettings pagingSettings,
            IMessageBoxService messageBoxService,
            FileLogger fileLogger)
        {
            _fileDialogServices = fileDialogServices;
            _initializeExcelFile = initializeExcelFile;
            _exportSettings = exportSettings;
            _worksheet = worksheet;
            _search = search;
            _dataBase = dataBase;
            _paginator = paginator;
            _pagingSettings = pagingSettings;
            _messageBoxService = messageBoxService;
            _fileLogger = fileLogger;
        }

        /// <summary>
        /// Manages export state, create a excel file
        /// initializes and fills the file with data
        /// </summary>
        public async Task Export()
        {

            await _messageBoxService.ShowProgressAsync(
                "Export to Excel",
                "Exporting data, please wait...",
                async controller =>
                {
                    try
                    {
                        _exportSettings.IsExporting = true;

                        var fileName = _fileDialogServices.CreateFile("Excel Files|*.xlsx", "Choose Excel file");
                        if (string.IsNullOrEmpty(fileName))
                        {
                            InvalidPath?.Invoke(this, EventArgs.Empty);
                            return;
                        }

                        _exportSettings.ExcelFileName = fileName;
                        _initializeExcelFile.InitializeFile(fileName);

                        await Task.Run(() => _worksheet.Fill(_dataBase, _exportSettings, _search,
                                _paginator, _pagingSettings,
                                count => controller.SetProgress(count))
                        );

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
                },
                false
            );
        }
    }
}