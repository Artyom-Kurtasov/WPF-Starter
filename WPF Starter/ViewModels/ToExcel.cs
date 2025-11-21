using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using ClosedXML.Excel;
using WPF_Starter.Models;

namespace WPF_Starter.ViewModels
{
    public class ToExcel
    {
        private readonly States _states;
        private readonly SaveFileDialog _saveFileDialog = new SaveFileDialog();
        public ToExcel(States states) 
        {
            _states = states;

           _saveFileDialog = new SaveFileDialog
            {
                Filter = "Excel Files|*.xlsx",
                DefaultExt = ".xlsx",
                FileName = $"People_Export_{DateTime.Now:yyyyMMdd_HHmmss}"
            };
        }
        public void ConvertToExcel()
        {
            if (_saveFileDialog.ShowDialog() == true)
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("People");

                    worksheet.Cell(1, 1).Value = "ID";
                    worksheet.Cell(1, 2).Value = "Name";
                    worksheet.Cell(1, 3).Value = "Surname";
                    worksheet.Cell(1, 4).Value = "Patronymic";
                    worksheet.Cell(1, 5).Value = "City";
                    worksheet.Cell(1, 6).Value = "Country";

                    for (int i = 0; i < _states.AllSov.Count; i++)
                    {
                        worksheet.Cell(i + 2, 1).Value = _states.AllSov[i].Id;
                        worksheet.Cell(i + 2, 2).Value = _states.AllSov[i].Name;
                        worksheet.Cell(i + 2, 3).Value = _states.AllSov[i].Surname;
                        worksheet.Cell(i + 2, 4).Value = _states.AllSov[i].Patronymic;
                        worksheet.Cell(i + 2, 5).Value = _states.AllSov[i].City;
                        worksheet.Cell(i + 2, 6).Value = _states.AllSov[i].Country;

                    }

                    workbook.SaveAs(_saveFileDialog.FileName);
                }
            }
        }
    }
}
