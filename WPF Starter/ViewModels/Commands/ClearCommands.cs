using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using WPF_Starter.Models;

namespace WPF_Starter.ViewModels.Commands
{
    public class ClearCommands : INotifyPropertyChanged
    {
        private readonly States _states;
        public ICommand ClearDatePickerCommand { get; }
        public ICommand ClearNameCommand { get; }
        public ICommand ClearSurnameCommand { get; }
        public ICommand ClearPatronymicCommand { get; }
        public ICommand ClearCityCommand { get; }
        public ICommand ClearCountryCommand { get; }

        public ClearCommands(States states)
        {
            ClearDatePickerCommand = new RelayCommands(ClearDatePicker, CanClearDatePicker);
            ClearNameCommand = new RelayCommands(ClearNameTextBox, CanClearNameTextBox);
            ClearSurnameCommand = new RelayCommands(ClearSurnameTextBox, CanClearSurnameTextBox);
            ClearPatronymicCommand = new RelayCommands(ClearPatronymicTextBox, CanClearPatronymicTextBox);
            ClearCityCommand = new RelayCommands(ClearCityTextBox, CanClearCityTextBox);
            ClearCountryCommand = new RelayCommands(ClearCountryTextBox, CanClearCountryTextBox);
            _states = states;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void ClearDatePicker() => _states.DateOfDatepicker = null;
        private void ClearNameTextBox() => _states.NameBoxText = string.Empty;
        private void ClearSurnameTextBox() => _states.SurnameBoxText = string.Empty;
        private void ClearPatronymicTextBox() => _states.PatronymicBoxText = string.Empty;
        private void ClearCityTextBox() => _states.CityBoxText = string.Empty;
        private void ClearCountryTextBox() => _states.CountryBoxText = string.Empty;

        private bool CanClearDatePicker() => true;
        private bool CanClearNameTextBox() => true;
        private bool CanClearSurnameTextBox() => true;
        private bool CanClearPatronymicTextBox() => true;
        private bool CanClearCityTextBox() => true;
        private bool CanClearCountryTextBox() => true;
    }
}
