using System.ComponentModel;

namespace WPF_Starter.Models
{
    public class PagingSettings : INotifyPropertyChanged
    {
        private List<People>? _gridPeoples;
        public List<People>? GridPeoples
        {
            get => _gridPeoples;
            set
            {
                _gridPeoples = value;
                OnPropertyChanged(nameof(GridPeoples));
            }
        }

        private string? _pageIndicator;
        public string? PageIndicator
        {
            get => _pageIndicator;
            set
            {
                _pageIndicator = value;
                OnPropertyChanged(nameof(PageIndicator));
            }
        }
        public int Page { get; set; } = 0;
        public int PageSize { get; } = 1000;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
