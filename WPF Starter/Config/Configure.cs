using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WPF_Starter.Models;
using WPF_Starter.Services;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.DataGridServices;
using WPF_Starter.Services.DialogServices;
using WPF_Starter.Services.DialogServices.Interfaces;
using WPF_Starter.Services.Export;
using WPF_Starter.Services.FileServices;
using WPF_Starter.Services.Import;
using WPF_Starter.Services.MessageServices;
using WPF_Starter.Services.MessageServices.Interfaces;
using WPF_Starter.Services.Notifiers;
using WPF_Starter.Services.SearchServices;
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
            services.AddSingleton<ExportSettings>();
            services.AddSingleton<PeopleFormState>();
            services.AddSingleton<PagingSettings>();
            services.AddSingleton<DataGridManager>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<DataBaseNotifier>();
            services.AddSingleton<ExportNotifyer>();
            services.AddSingleton<DataBaseNotifier>();
            services.AddSingleton<ErrorNotifier>();
            services.AddSingleton<ImportNotifier>();
            services.AddSingleton<StartupDataLoader>();
            services.AddSingleton<IMessageBoxService, MessageBoxService>();
            services.AddSingleton<LoadingState>();
            services.AddSingleton<ExportToExcel>();
            services.AddSingleton<ExportToXml>();
            services.AddSingleton<ImportCsv>();
            services.AddSingleton<ImportCommands>();
            services.AddSingleton<ExportWindowInitialization>();
            services.AddSingleton<ExportWindowViewModel>();


            services.AddTransient<RefreshStates>();
            services.AddTransient<GridDataService>();
            services.AddTransient<Search>();
            services.AddTransient<InitializeXmlFile>();
            services.AddTransient<InitializeExcelFile>();
            services.AddTransient<Paginator>();
            services.AddTransient<FillWorksheet>();
            services.AddTransient<CreateRootElement>();
            services.AddTransient<DataBaseWriter>();
            services.AddTransient<CsvParser>();
            services.AddTransient<IFileDialogService, FileDialogService>();
            services.AddTransient<ExportCommands>();
            services.AddTransient<ClearCommands>();
            services.AddTransient<NavigationCommands>();
            services.AddTransient<RelayCommand>();
            services.AddTransient<AsyncRelayCommand>();
            services.AddTransient<MainWindow>();
            services.AddTransient<Export>();
            services.AddTransient<MainWindowInitialization>();
            services.AddTransient<CsvFileReader>();
            services.AddTransient<CsvRowParser>();
            services.AddTransient<PeopleMapper>();
        }
    }
}
