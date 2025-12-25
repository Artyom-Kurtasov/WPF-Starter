using System.Collections.ObjectModel;
using WPF_Starter.Config.Settings;
using WPF_Starter.Services;


namespace WPF_Starter.Models
{
    public class PagingSettings : PropertyChangedEvent
    {
        private UserSettings _userSettings;

        public PagingSettings(UserSettings userSettings)
        {
            _userSettings = userSettings;
        }

        private ObservableCollection<People>? _gridPeoples;
        public ObservableCollection<People>? GridPeoples
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

        private int _pageSize = UserSettings.Default.PageSize;
        public int PageSize
        {
            get => _pageSize;
            set
            {
                if (_pageSize != value)
                {
                    _pageSize = value;            
                    OnPropertyChanged(nameof(PageSize));

                    _userSettings.PageSize = value;
                    _userSettings.Save();
                }
            }
        }

        private int _blockSize  = UserSettings.Default.BlockSize;
        public int BlockSize
        {
            get => _blockSize;
            set
            {
                _blockSize = value;
                OnPropertyChanged(nameof(BlockSize));

                _userSettings.BlockSize = value;
                _userSettings.Save();
            }
        }
    }
}
