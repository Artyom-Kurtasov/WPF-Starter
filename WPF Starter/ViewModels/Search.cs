using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using WPF_Starter.DataBase;
using WPF_Starter.Models;
using WPF_Starter.View;
using System.Data.SqlClient;

namespace WPF_Starter.ViewModels
{
    public class Search
    {
        private States _states;

        public Search(States states)
        {
            _states = states;
        }

        public void GetSearchText(string searchText)
        {
            _states.TextFromSeacrhBox = searchText.Split(" ");
        }

        public void GetFilteredList()
        {
            using (var dataBase = new AppDbContext())
            {
                var allPeople = dataBase.Person.ToList();

                _states.AllSov = allPeople 
                     .Where(person => _states.TextFromSeacrhBox.All(term =>
                        person.Name.Contains(term) ||
                        person.Surname.Contains(term) ||
                        person.Patronymic.Contains(term) ||
                        person.City.Contains(term) ||
                        person.Country.Contains(term) ||
                        person.Date.ToString("dd.MM.yyyy").Contains(term) ||  // 09.12.2025
                        person.Date.ToString("dd/MM/yyyy").Contains(term) ||  // 09/12/2025
                        person.Date.ToString("d.M.yyyy").Contains(term) ||    // 9.12.2025  
                        person.Date.ToString("d/M/yyyy").Contains(term) ||    // 9/12/2025
                        person.Date.ToString("yyyy-MM-dd").Contains(term)))
                     .ToList();
            }
        }
    }
}
