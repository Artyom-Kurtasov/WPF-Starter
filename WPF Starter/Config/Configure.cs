using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WPF_Starter.Models;
using WPF_Starter.Services;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.DataGridServices;
using WPF_Starter.Services.DialogServices;
using WPF_Starter.Services.DialogServices.Interfaces;
using WPF_Starter.Services.Export;
using WPF_Starter.Services.Export.Interfaces;
using WPF_Starter.Services.FileServices;
using WPF_Starter.Services.Import;
using WPF_Starter.Services.Import.Interfaces;
using WPF_Starter.Services.Logging;
using WPF_Starter.Services.MessageServices;
using WPF_Starter.Services.MessageServices.Interfaces;
using WPF_Starter.Services.Notifiers;
using WPF_Starter.Services.SearchServices;
using WPF_Starter.View;
using WPF_Starter.ViewModels;
using WPF_Starter.ViewModels.Commands;
using WPF_Starter.Config.Settings;
using WPF_Starter.Services.ValidationRules;
using MahApps.Metro.Controls.Dialogs;
using WPF_Starter.Config.Initialization;

namespace WPF_Starter.Config
{
    public class Configure
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //Data
            services.AddDbContext<AppDbContext>(options => 
                options.UseSqlServer(UserSettings.Default.ConnectionString), ServiceLifetime.Transient);
            services.AddSingleton<People>();
            services.AddSingleton<ExportSettings>();
            services.AddSingleton<PeopleFormState>();
            services.AddSingleton<PagingSettings>();
            services.AddSingleton<UserSettings>();
            services.AddSingleton<ConnectionStringValidator>();
            services.AddSingleton<IDialogCoordinator>(DialogCoordinator.Instance);

            //ViewModels
            services.AddSingleton<SettingsWindowViewModel>();
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<ExportWindowViewModel>();

            //Windows
            services.AddSingleton<MainWindow>();
            services.AddSingleton<Export>();
            services.AddSingleton<SettingsWindow>();

            //Initialization
            services.AddTransient<MainWindowInitialization>();
            services.AddSingleton<ExportWindowInitialization>();
            services.AddSingleton<SettingsWindowInitialization>();

            //Commands
            services.AddSingleton<SettingCommands>();
            services.AddTransient<ExportCommands>();
            services.AddTransient<ImportCommands>();
            services.AddTransient<NavigationCommands>();
            services.AddTransient<RelayCommand>();
            services.AddTransient<AsyncRelayCommand>();

            //Services
            services.AddSingleton<DataGridManager>();
            services.AddTransient<GridDataService>();
            services.AddTransient<Search>();
            services.AddTransient<Paginator>();
            services.AddTransient<IFileDialogService, FileDialogService>();
            services.AddSingleton<IMessageBoxService, MessageBoxService>();
            services.AddTransient<PropertyChangedEvent>();
            services.AddSingleton<FileLogger>();
            services.AddSingleton<Validator>();

            //Services: Import/Export
            services.AddSingleton<IImportCsv, ImportCsv>();
            services.AddSingleton<IExportToExcel, ExportToExcel>();
            services.AddSingleton<IExportToXml, ExportToXml>();
            services.AddTransient<InitializeXmlFile>();
            services.AddTransient<InitializeExcelFile>();
            services.AddTransient<FillWorksheet>();
            services.AddTransient<CreateRootElement>();

            //Services: DataBase
            services.AddSingleton<DatabaseCreator>();
            services.AddTransient<DataBaseWriter>();
            services.AddTransient<CsvParser>();
            services.AddTransient<CsvFileReader>();
            services.AddTransient<CsvRowParser>();
            services.AddTransient<PeopleMapper>();

            //Notifiers
            services.AddSingleton<ExportNotifyer>();
            services.AddSingleton<ErrorNotifier>();
            services.AddSingleton<ImportNotifier>();
            services.AddSingleton<DatabaseNotifier>();

            //Startup
            services.AddSingleton<IStartupDataLoader, StartupDataLoader>();
        }
    }
}
