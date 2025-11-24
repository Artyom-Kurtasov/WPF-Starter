using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using WPF_Starter.Models;

namespace WPF_Starter.ViewModels
{
    public class ClearFieldsExportForm
    {
        private States _states;
        public ClearFieldsExportForm(States states)
        {
            _states = states;
        }

        public void ClearFields()
        {
            _states.NameBoxText = string.Empty;
            _states.SurnameBoxText = string.Empty;
            _states.PatronymicBoxText = string.Empty;
            _states.CityBoxText = string.Empty;
            _states.CountryBoxText = string.Empty;
            _states.DateBoxText = null;
        }
    }
}
