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

namespace WPF_Starter.Config
{
    public class Configure
    {
        public void conf(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer("Server=NINJA2077\\MSSQLCSV;Database=Person;Trusted_Connection=True;TrustServerCertificate=True;"));
            services.AddSingleton<People>();
            services.AddSingleton<States>();
            services.AddSingleton<CsvLoader>();
            services.AddSingleton<DataBaseLoader>();
            services.AddSingleton<ToXml>();
            services.AddSingleton<ToExcel>();
            services.AddSingleton<Search>();
            services.AddSingleton<DataGridManager>();
            services.AddSingleton<Commands>();

            services.AddTransient<ClearFieldsExportForm>();
            services.AddTransient<RelayCommands>();
            services.AddTransient<SaveFileDialog>();
            services.AddTransient<MainWindow>();
            services.AddTransient<ExportToExcel>();
            services.AddTransient<MainWindowInitialization>();
            services.AddTransient<StartupDataLoader>();

        }
    }
}
