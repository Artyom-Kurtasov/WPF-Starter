using System.Windows.Input;
using WPF_Starter.Models;

namespace WPF_Starter.ViewModels.Commands
{
    public class ClearCommands
    {
        private readonly PeopleFormState _peopleFormState;
        public ICommand ClearDatePickerCommand { get; }
        public ICommand ClearNameCommand { get; }
        public ICommand ClearSurnameCommand { get; }
        public ICommand ClearPatronymicCommand { get; }
        public ICommand ClearCityCommand { get; }
        public ICommand ClearCountryCommand { get; }

        public ClearCommands(PeopleFormState peopleFormState)
        {
            ClearDatePickerCommand = new RelayCommand(ClearDatePicker, CanClearDatePicker);
            ClearNameCommand = new RelayCommand(ClearNameTextBox, CanClearNameTextBox);
            ClearSurnameCommand = new RelayCommand(ClearSurnameTextBox, CanClearSurnameTextBox);
            ClearPatronymicCommand = new RelayCommand(ClearPatronymicTextBox, CanClearPatronymicTextBox);
            ClearCityCommand = new RelayCommand(ClearCityTextBox, CanClearCityTextBox);
            ClearCountryCommand = new RelayCommand(ClearCountryTextBox, CanClearCountryTextBox);
            _peopleFormState = peopleFormState;
        }
        private void ClearDatePicker() => _peopleFormState.DateOfDatepicker = null;
        private void ClearNameTextBox() => _peopleFormState.NameBoxText = string.Empty;
        private void ClearSurnameTextBox() => _peopleFormState.SurnameBoxText = string.Empty;
        private void ClearPatronymicTextBox() => _peopleFormState.PatronymicBoxText = string.Empty;
        private void ClearCityTextBox() => _peopleFormState.CityBoxText = string.Empty;
        private void ClearCountryTextBox() => _peopleFormState.CountryBoxText = string.Empty;

        private bool CanClearDatePicker() => true;
        private bool CanClearNameTextBox() => true;
        private bool CanClearSurnameTextBox() => true;
        private bool CanClearPatronymicTextBox() => true;
        private bool CanClearCityTextBox() => true;
        private bool CanClearCountryTextBox() => true;
    }
}
