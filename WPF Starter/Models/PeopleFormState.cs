    using WPF_Starter.Services;

    namespace WPF_Starter.Models
    {
        public class PeopleFormState : PropertyChangedEvent
        {
            private DateTime? _dateBoxText;
            public DateTime? DateOfDatepicker
            {
                get => _dateBoxText;
                set
                {
                    _dateBoxText = value;
                    OnPropertyChanged(nameof(DateOfDatepicker));
                }
            }

            private string? _nameBoxText;
            public string? NameBoxText
            {
                get => _nameBoxText;
                set
                {
                    _nameBoxText = value;
                    OnPropertyChanged(nameof(NameBoxText));
                }
            }

            private string? _surnameBoxText;
            public string? SurnameBoxText
            {
                get => _surnameBoxText;
                set
                {
                    _surnameBoxText = value;
                    OnPropertyChanged(nameof(SurnameBoxText));
                }
            }

            private string? _patronymicBoxText;
            public string? PatronymicBoxText
            {
                get => _patronymicBoxText;
                set
                {
                    _patronymicBoxText = value;
                    OnPropertyChanged(nameof(PatronymicBoxText));
                }
            }

            private string? _cityBoxText;
            public string? CityBoxText
            {
                get => _cityBoxText;
                set
                {
                    _cityBoxText = value;
                    OnPropertyChanged(nameof(CityBoxText));
                }
            }

            private string? _countryBoxText;
            public string? CountryBoxText
            {
                get => _countryBoxText;
                set
                {
                    _countryBoxText = value;
                    OnPropertyChanged(nameof(CountryBoxText));
                }
            }
        }
    }
