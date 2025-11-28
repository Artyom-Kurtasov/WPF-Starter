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
    public class ExportToXml
    {
        private readonly PagingSettings _pagingSettings;
        private readonly Paginator _paginator;
        private readonly Search _search;
        private readonly AppDbContext _dataBase;
        private readonly CreateRootElement _createRootElement;
        private readonly ExportSettings _exportSettings;
        private readonly InitializeXmlFile _initializeXmlFile;
        private readonly IFileDialogService _fileDialogServices;

        public ExportToXml(IFileDialogService fileDialogServices, InitializeXmlFile initializeXmlFile, ExportSettings exportSettings, CreateRootElement createRootElement,
            AppDbContext dataBase, Search search, Paginator paginator, PagingSettings pagingSettings)
        {
            _fileDialogServices = fileDialogServices;
            _initializeXmlFile = initializeXmlFile;
            _exportSettings = exportSettings;
            _search = search;
            _dataBase = dataBase;
            _paginator = paginator;
            _createRootElement = createRootElement;
            _pagingSettings = pagingSettings;
        }
        public void Export()
        {
            _exportSettings.XmlFileName = _fileDialogServices.CreateFile("XML Files|*.xml", "Choose Xml file");
            _initializeXmlFile.InitializeFile(_exportSettings.XmlFileName);
            _createRootElement.Fill(_dataBase, _exportSettings, _search, _paginator, _pagingSettings);
        }
    }
}
