using System;
using System.Collections.Generic;
using System.Text;
using WPF_Starter.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using WPF_Starter.DataBase;
using System.Globalization;

namespace WPF_Starter.ViewModels.DataBaseServices
{
    public class DataBaseLoader
    {
        private readonly DataBaseWriter _writer;
        public DataBaseLoader(DataBaseWriter dataBaseWriter) 
        {
            _writer = dataBaseWriter;
        }
        public void LoadDataBase(AppDbContext dataBase)
        {
            try
            {
                _writer.Save(dataBase);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.ToString(), "Ошибка");
            }
        }
    }
}
