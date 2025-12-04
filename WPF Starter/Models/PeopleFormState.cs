using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF_Starter.Models
{
    public class PeopleFormState : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        
        private DateTime? _dateBoxText;
        public DateTime? DateOfDatepicker
        {
            get => _dateBoxText;
            set
            {
                _dateBoxText = value;
                OnPropertyChanged();
            }
        }

        private string? _nameBoxText;
        public string? NameBoxText
        {
            get => _nameBoxText;
            set
            {
                _nameBoxText = value;
                OnPropertyChanged();
            }
        }

        private string? _surnameBoxText;
        public string? SurnameBoxText
        {
            get => _surnameBoxText;
            set
            {
                _surnameBoxText = value;
                OnPropertyChanged();
            }
        }

        private string? _patronymicBoxText;
        public string? PatronymicBoxText
        {
            get => _patronymicBoxText;
            set
            {
                _patronymicBoxText = value;
                OnPropertyChanged();
            }
        }

        private string? _cityBoxText;
        public string? CityBoxText
        {
            get => _cityBoxText;
            set
            {
                _cityBoxText = value;
                OnPropertyChanged();
            }
        }

        private string? _countryBoxText;
        public string? CountryBoxText
        {
            get => _countryBoxText;
            set
            {
                _countryBoxText = value;
                OnPropertyChanged();
            }
        }
    }
}
