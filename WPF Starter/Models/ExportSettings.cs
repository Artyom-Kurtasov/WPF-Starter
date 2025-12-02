using System.ComponentModel;

namespace WPF_Starter.Models
{
    public class ExportSettings : INotifyPropertyChanged
    {
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
        public int MaxExcelRows { get; private set; } = 1048576;
        public string? ExcelFileName { get; set; } = null;
        public string? CsvFilePath { get; set; } = "people.csv";
        public string? XmlFileName { get; set; } = null;


        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
