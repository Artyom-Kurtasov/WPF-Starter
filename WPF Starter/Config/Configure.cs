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
            services.AddSingleton<ExportToXml>();
            services.AddSingleton<ExportToExcel>();
            services.AddSingleton<Search>();
            services.AddSingleton<DataGridManager>();
            services.AddSingleton<MainWindowViewModel>();

            services.AddTransient<ExportCommands>();
            services.AddTransient<ClearCommands>();
            services.AddTransient<NavigationCommands>();
            services.AddTransient<RelayCommands>();
            services.AddTransient<SaveFileDialog>();
            services.AddTransient<MainWindow>();
            services.AddTransient<Export>();
            services.AddTransient<MainWindowInitialization>();
            services.AddTransient<StartupDataLoader>();

        }
    }
}
