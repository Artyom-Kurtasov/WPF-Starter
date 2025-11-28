using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using WPF_Starter.ViewModels.Interfaces;

namespace WPF_Starter.ViewModels.FileServices
{
    public  class InitializeXmlFile
    {
        public void InitializeFile(string fileName)
        {
            var doc = new XDocument(new XElement("Peoples"));
            doc.Save(fileName);
        }
    }
}
