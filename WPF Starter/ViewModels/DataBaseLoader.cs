using System;
using System.Collections.Generic;
using System.Text;
using WPF_Starter.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using WPF_Starter.DataBase;

namespace WPF_Starter.ViewModels
{
    public class DataBaseLoader
    {
        private readonly States _states;
        public DataBaseLoader(States states, People people) 
        {
            _states = states;
        }
        private void SetListOfData()
        {
            var lines = File.ReadAllLines(_states.CsvFileName);

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] cells = line.Split(';');
                if (cells.Length < 6) continue;

                if (!DateOnly.TryParseExact(cells[0],
                    new[] { "dd/MM/yyyy", "d/M/yyyy", "dd.MM.yyyy", "yyyy-MM-dd" },
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None,
                    out var date))
                {
                    continue; 
                }

                People person = new()
                {
                    Date = date,
                    Name = cells[1],
                    Surname = cells[2],
                    Patronymic = cells[3],
                    City = cells[4],
                    Country = cells[5]
                };

                _states.peoples.AddLast(person);
            }
        }

        private void AddToDataBase(AppDbContext dataBase)
        {
                var deletable = dataBase.Person.Where(u => u.Id != null);

                dataBase.Person.RemoveRange(deletable);
                foreach(var people in _states.peoples)
                {
                    dataBase.Person.Add(people);
                }
                dataBase.SaveChanges();
            }
        public void LoadDataBase(AppDbContext dataBase)
        {
            try
            {
                SetListOfData();
                AddToDataBase(dataBase);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Ошибка");
            }
        }
    }
}
