using System.Xml.Linq;
using WPF_Starter.Models;
using WPF_Starter.Services.DataBase;
using WPF_Starter.Services.SearchServices;

namespace WPF_Starter.Services.FileServices
{
    public class CreateRootElement
    {

        /// <summary>
        /// Creates new xml file
        /// add data to the file and saves changes
        /// </summary>
        public void Fill(AppDbContext dataBase, ExportSettings exportSettings, Search search, Paginator paginator, PagingSettings pagingSettings)
        {
            XDocument? doc = XDocument.Load(exportSettings.XmlFileName);

            foreach (List<People> batch in paginator.Pagenation(dataBase, pagingSettings, search.SearchPeople(dataBase)))
            {
                foreach (People? person in batch)
                {
                    XElement personElement = new XElement("Person",
                        new XElement("Date", $"{person.Date}"),
                        new XElement("Name", $"{person.Name}"),
                        new XElement("Surname", $"{person.Surname}"),
                        new XElement("Patronymic", $"{person.Patronymic}"),
                        new XElement("City", $"{person.City}"),
                        new XElement("Country", $"{person.Country}"));
                    doc.Root.Add(personElement);
                }
            }

            doc.Save(exportSettings.XmlFileName);
        }
    }
}
