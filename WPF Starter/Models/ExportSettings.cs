using WPF_Starter.Config.Settings;
using WPF_Starter.Services;
using WPF_Starter.Services.DataBase;

namespace WPF_Starter.Models
{
    public class ExportSettings : PropertyChangedEvent
    {
        private UserSettings? _userSettings;
        public ExportSettings(UserSettings? userSettings)
        {
            _userSettings = userSettings;
        }

        private const int _percentOfRowsCountMaximum = 100;
        private double _percentOfRows;
        public double PercentOfRows
        {
            get => _percentOfRows;
            set
            {
                _percentOfRows = value / _percentOfRowsCountMaximum; 
                OnPropertyChanged(nameof(PercentOfRows));
            }
        }

        private string _bufferSize = UserSettings.Default.BufferSize.ToString();
        public string BufferSize
        {
            get => _bufferSize;
            set
            {
                _bufferSize = value;

                OnPropertyChanged(nameof(BufferSize));

                if (int.TryParse(value, out var parsed) && parsed > 0)
                {
                    _userSettings!.BufferSize = parsed;
                    _userSettings.Save();
                }
            }
        }
        private int _maxExcelRows = UserSettings.Default.MaxExcelRows;
        public int MaxExcelRows
        {
            get => _maxExcelRows;
            set
            {
                _maxExcelRows = value;
                OnPropertyChanged(nameof(MaxExcelRows));

                _userSettings?.MaxExcelRows = value;
                _userSettings?.Save();
            }
        }

        private bool _isExporting;
        public bool IsExporting
        {
            get => _isExporting;
            set
            {
                _isExporting = value;
                OnPropertyChanged(nameof(IsExporting));
            }
        }
        public string? CsvFilePath { get; set; } = "people.csv";
        public string? ExcelFileName { get; set; } = null;
        public string? XmlFileName { get; set; } = null;
    }
}
