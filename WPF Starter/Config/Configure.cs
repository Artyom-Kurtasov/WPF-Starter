using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using WPF_Starter.DataBase;
using WPF_Starter.Models;
using WPF_Starter.View;
using WPF_Starter.ViewModels;
using WPF_Starter.ViewModels.Commands;
using WPF_Starter.ViewModels.DataBaseServices;
using WPF_Starter.ViewModels.DialogServices;
using WPF_Starter.ViewModels.ExportData;
using WPF_Starter.ViewModels.FileServices;
using WPF_Starter.ViewModels.Interfaces;
using WPF_Starter.ViewModels.SearchServices;

namespace WPF_Starter.Config
{
    public class Configure
    {
        public void conf(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("Server=NINJA2077\\MSSQLCSV;Database=Person;Trusted_Connection=True;TrustServerCertificate=True;"));
            services.AddSingleton<People>();
            services.AddSingleton<ExportSettings>();
            services.AddSingleton<PeopleFormState>();
            services.AddSingleton<PagingSettings>();
            services.AddSingleton<DataGridManager>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddTransient<Search>();
            services.AddTransient<FilesLoader>();
            services.AddTransient<InitializeXmlFile>();
            services.AddTransient<InitializeExcelFile>();
            services.AddTransient<ExportToExcel>();
            services.AddTransient<ExportToXml>();
            services.AddTransient<Paginator>();
            services.AddTransient<FillWorksheet>();
            services.AddTransient<CreateRootElement>();
            services.AddTransient<DataBaseLoader>();
            services.AddTransient<DataBaseWriter>();
            services.AddTransient<CsvParser>();
            services.AddTransient<IFileDialogService, FileDialogService>();
            services.AddTransient<ExportCommands>();
            services.AddTransient<ClearCommands>();
            services.AddTransient<NavigationCommands>();
            services.AddTransient<RelayCommands>();
            services.AddTransient<MainWindow>();
            services.AddTransient<Export>();
            services.AddTransient<MainWindowInitialization>();
            services.AddTransient<StartupDataLoader>();
        }
    }
}
