using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.Notifiers;
using WPF_Starter.Services.ValidationRules;
using WPF_Starter.ViewModels.Commands;

namespace WPF_Starter.ViewModels
{
    public class SettingsWindowViewModel
    {
        public SettingCommands SettingCommands { get; }
        public ExportSettings ExportSettings { get; }
        public PagingSettings PagingSettings { get; }
        public Validator Validator{ get; }
        public ConnectionStringValidator ConnectionStringValidator { get; }

        public SettingsWindowViewModel(SettingCommands settingCommands, PagingSettings pagingSettings, ExportSettings exportSettings,
            Validator validator, ConnectionStringValidator connectionStringValidator, DatabaseNotifier databaseNotifier)
        {
            SettingCommands = settingCommands;
            PagingSettings = pagingSettings;
            ExportSettings = exportSettings;
            Validator = validator;
            ConnectionStringValidator = connectionStringValidator;
        }
    }
}
