using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using ClosedXML.Excel;
using WPF_Starter.Models;
using System.Threading.Tasks.Dataflow;
using WPF_Starter.DataBase;
using System.Windows;

namespace WPF_Starter.ViewModels
{
    public class ToExcel
    {
        private readonly Search _search;
        private readonly States _states;
        private readonly SaveFileDialog _saveFileDialog = new SaveFileDialog();
        public ToExcel(States states, Search search, AppDbContext appDbContext)
        {
            _states = states;
            _search = search;
        }
        private void SetDialogFilter()
        {
            _saveFileDialog.Filter = "Excel Files|*.xlsx";
            _saveFileDialog.DefaultExt = ".xlsx";
        }
        private void GetExcelFileName()
        {
            _states.ExcelFileName = _saveFileDialog.ShowDialog() == true ? _saveFileDialog.FileName : null;
        }
        private void CreateExcelFile()
        {
            using (var workbook = new XLWorkbook())
            {
                workbook.AddWorksheet("Data");
                workbook.SaveAs(_states.ExcelFileName);
            }
        }
        private IEnumerable<List<People>> PagenationList(AppDbContext dataBase)
        {

            var query = _search.SearchPeople(dataBase);
            _states.Page = 0;

            while (true)
            {
                var batch = query
                    .OrderBy(x => x.Date)
                    .Skip(_states.Page * _states.PageSize)
                    .Take(_states.PageSize)
                    .ToList();

                if (!batch.Any()) yield break;

                _states.Page++;

                yield return batch;
            }
        }
        private void FillWorksheet(AppDbContext dataBase)
        {
            using (var workbook = new XLWorkbook(_states.ExcelFileName))
            {
                var worksheet = workbook.Worksheet("Data");
                int row = 2;

                worksheet.Cell(1, 1).Value = "Date";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "Surname";
                worksheet.Cell(1, 4).Value = "Patronymic";
                worksheet.Cell(1, 5).Value = "City";
                worksheet.Cell(1, 6).Value = "Country";

                foreach (var batch in PagenationList(dataBase))
                {
                    foreach (var item in batch)
                    {
                        worksheet.Cell(row, 1).Value = item.Date.ToDateTime(TimeOnly.MinValue);
                        worksheet.Cell(row, 1).Style.DateFormat.Format = "dd.MM.yyyy";
                        worksheet.Cell(row, 2).Value = item.Name;
                        worksheet.Cell(row, 3).Value = item.Surname;
                        worksheet.Cell(row, 4).Value = item.Patronymic;
                        worksheet.Cell(row, 5).Value = item.City;
                        worksheet.Cell(row, 6).Value = item.Country;
                        row++;
                    }
                }
                workbook.Save();

            }
        }
        public void FillExcelFile(AppDbContext dataBase)
        {
            SetDialogFilter();
            GetExcelFileName();
            CreateExcelFile();
            FillWorksheet(dataBase);
        }
    }
}
