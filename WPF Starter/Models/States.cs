using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using WPF_Starter.View;

namespace WPF_Starter.Models
{
    public class States : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public LinkedList<People> peoples = new();
        public List<People> AllSov = new();
        public string? CsvFileName { get; set; }
        public string? XmlFileName { get; set; }
        private DateTime? _dateBoxText;
        public DateTime? DateBoxText
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
        public int Page { get; set; } = 0;
        public int PageSize { get; } = 1000;
        public string? ExcelFileName { get; set; } = null;

    }
}
