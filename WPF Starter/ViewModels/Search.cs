using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using WPF_Starter.DataBase;
using WPF_Starter.Models;
using WPF_Starter.View;
using System.Data.SqlClient;
using System.Windows;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using System.Globalization;

namespace WPF_Starter.ViewModels
{
    public class Search
    {
        private States _states;

        public Search(States states)
        {
            _states = states;
        }
        public IQueryable<People> SearchPeople(AppDbContext dataBase)
        {
            var nameFilter = _states.NameBoxText?.Trim().ToLower();
            var surnameFilter = _states.SurnameBoxText?.Trim().ToLower();
            var patronymicFilter = _states.PatronymicBoxText?.Trim().ToLower();
            var cityFilter = _states.CityBoxText?.Trim().ToLower();
            var countryFilter = _states.CountryBoxText?.Trim().ToLower();

            DateTime? parsedDate = _states.DateOfDatepicker;

            return dataBase.Person.Where(u =>
                (_states.DateOfDatepicker == null ||u.Date == parsedDate) &&
                (string.IsNullOrEmpty(nameFilter) || u.Name.ToLower().Contains(nameFilter)) &&
                (string.IsNullOrEmpty(surnameFilter) || u.Surname.ToLower().Contains(surnameFilter)) &&
                (string.IsNullOrEmpty(patronymicFilter) || u.Patronymic.ToLower().Contains(patronymicFilter)) &&
                (string.IsNullOrEmpty(cityFilter) || u.City.ToLower().Contains(cityFilter)) &&
                (string.IsNullOrEmpty(countryFilter) || u.Country.ToLower().Contains(countryFilter))
            );
        }

    }
}
