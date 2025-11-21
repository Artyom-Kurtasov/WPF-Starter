using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using WPF_Starter.Models;
using WPF_Starter.View;
using WPF_Starter.ViewModels;

namespace WPF_Starter.Config
{
    public class Configure
    {
        public void conf(IServiceCollection services)
        {
            services.AddScoped<People>();
            services.AddScoped<States>();
            services.AddScoped<CsvLoader>();
            services.AddScoped<DataBaseLoader>();
            services.AddScoped<ToExcel>();
            services.AddScoped<Search>();

            services.AddTransient<SaveFileDialog>();
            services.AddScoped<DataGridManager>();
            services.AddSingleton<Commands>();
            services.AddScoped<RelayCommands>();

            services.AddTransient<MainWindow>();
            services.AddTransient<ExportToExcel>();

        }
    }
}
