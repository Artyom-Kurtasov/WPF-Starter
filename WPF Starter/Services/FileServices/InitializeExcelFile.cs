using ClosedXML.Excel;

namespace WPF_Starter.Services.FileServices
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
