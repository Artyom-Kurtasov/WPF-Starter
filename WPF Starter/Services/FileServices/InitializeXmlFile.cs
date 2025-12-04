using System.Xml.Linq;

namespace WPF_Starter.Services.FileServices
{
    public  class InitializeXmlFile
    {
        public void InitializeFile(string fileName)
        {
            XDocument? doc = new XDocument(new XElement("Peoples"));
            doc.Save(fileName);
        }
    }
}
