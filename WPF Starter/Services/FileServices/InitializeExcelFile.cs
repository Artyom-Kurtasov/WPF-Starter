using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;

namespace WPF_Starter.Services.FileServices
{
    public class InitializeExcelFile
    {
        public void InitializeFile(string fileName)
        {
            using (XLWorkbook? workbook = new XLWorkbook())
            {
                workbook.AddWorksheet("Data");
                workbook.SaveAs(fileName);
            }
        }
    }
}
