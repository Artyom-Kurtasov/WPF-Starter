using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Text;
using WPF_Starter.ViewModels.Interfaces;

namespace WPF_Starter.ViewModels.FileServices
{
    public class InitializeExcelFile
    {
        public void InitializeFile(string fileName)
        {
            using (var workbook = new XLWorkbook())
            {
                workbook.AddWorksheet("Data");
                workbook.SaveAs(fileName);
            }
        }
    }
}
