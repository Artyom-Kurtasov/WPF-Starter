using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Animation;
using WPF_Starter.Models;

namespace WPF_Starter.ViewModels
{
    public class CsvLoader
    {
        private States? _states;
        private OpenFileDialog _openFileDialog = new OpenFileDialog();
        public CsvLoader(States states) 
        {
            _states = states;
        }
        public void ChooseFile()
        {
            SetDialogConfiguration();
            GetFileName();
        }
        private void GetFileName()
        {
            _states.File = _openFileDialog.ShowDialog() == true ? _openFileDialog.FileName : null;
        }

        private void SetDialogConfiguration()
        {
            _openFileDialog.Filter = "CSV Files|*.csv";
            _openFileDialog.Title = "Choose CSV file";
        }
    }
}
