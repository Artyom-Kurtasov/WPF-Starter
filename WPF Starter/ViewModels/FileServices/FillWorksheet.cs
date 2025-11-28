using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Text;
using WPF_Starter.DataBase;
using WPF_Starter.Models;
using WPF_Starter.ViewModels.Interfaces;
using WPF_Starter.ViewModels.SearchServices;

namespace WPF_Starter.ViewModels.FileServices
{
    public class FillWorksheet
    {
        public void Fill(AppDbContext dataBase, ExportSettings exportSettings, Search search, Paginator paginator, PagingSettings pagingSettings)
        {
            using (var workbook = new XLWorkbook(exportSettings.ExcelFileName))
            {
                var worksheet = workbook.Worksheet("Data");
                int row = 2;

                worksheet.Cell(1, 1).Value = "Date";
                worksheet.Cell(1, 2).Value = "Name";
                worksheet.Cell(1, 3).Value = "Surname";
                worksheet.Cell(1, 4).Value = "Patronymic";
                worksheet.Cell(1, 5).Value = "City";
                worksheet.Cell(1, 6).Value = "Country";

                foreach (var batch in paginator.Pagenation(dataBase,pagingSettings, search.SearchPeople(dataBase)))
                {
                    foreach (var item in batch)
                    {
                        worksheet.Cell(row, 1).Value = item.Date;
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
    }
}
