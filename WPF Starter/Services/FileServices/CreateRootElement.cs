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
            XDocument doc = new XDocument(new XElement("TestProgram"));

            int idCounter = 1;

            foreach (List<People> batch in paginator.Pagenation(dataBase, pagingSettings, search.SearchPeople(dataBase)))
            {
                foreach (People person in batch)
                {
                    XElement recordElement = new XElement("Record",
                        new XAttribute("id", idCounter++),
                        new XElement("Date", person.Date),
                        new XElement("FirstName", person.Name),
                        new XElement("LastName", person.Surname),
                        new XElement("SurName", person.Patronymic),
                        new XElement("City", person.City),
                        new XElement("Country", person.Country)
                    );

                    doc.Root.Add(recordElement);
                }
            }

            doc.Save(exportSettings.XmlFileName);
        }

    }
}
