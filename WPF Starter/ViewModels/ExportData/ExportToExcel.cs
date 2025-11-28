using System;
using System.Collections.Generic;
using System.Text;
using WPF_Starter.DataBase;
using WPF_Starter.Models;
using WPF_Starter.ViewModels.FileServices;
using WPF_Starter.ViewModels.Interfaces;
using WPF_Starter.ViewModels.SearchServices;

namespace WPF_Starter.ViewModels.ExportData
{
    public class ExportToExcel
    {
        private readonly PagingSettings _pagingSettings;
        private readonly Paginator _paginator;
        private readonly Search _search;
        private readonly AppDbContext _dataBase;
        private readonly FillWorksheet _worksheet;
        private readonly ExportSettings _exportSettings;
        private readonly InitializeExcelFile _initializeExcelFile;
        private readonly IFileDialogService _fileDialogServices;

        public ExportToExcel(IFileDialogService fileDialogServices, InitializeExcelFile initializeExcelFile, ExportSettings exportSettings,
            FillWorksheet worksheet, AppDbContext dataBase, Search search, Paginator paginator, PagingSettings pagingSettings)
        {
            _fileDialogServices = fileDialogServices;
            _initializeExcelFile = initializeExcelFile;
            _exportSettings = exportSettings;
            _worksheet = worksheet;
            _search = search;
            _dataBase = dataBase;
            _paginator = paginator;
            _pagingSettings = pagingSettings;
        }
        public void Export()
        {
            _exportSettings.ExcelFileName = _fileDialogServices.CreateFile("Excel Files|*.xlsx", "Choose Excel file");
            _initializeExcelFile.InitializeFile(_exportSettings.ExcelFileName);
            _worksheet.Fill(_dataBase, _exportSettings, _search, _paginator, _pagingSettings);
        }
    }
}
