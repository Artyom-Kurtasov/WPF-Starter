using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using WPF_Starter.DataBase;
using WPF_Starter.Models;
using System.Xml.Linq;

namespace WPF_Starter.ViewModels
{
    public class ExportToXml
    {
        private readonly Search _search;
        private readonly States _states;
        private readonly SaveFileDialog _saveFileDialog = new SaveFileDialog();

        public ExportToXml(States states, Search search)
        {
            _search = search;
            _states = states;
        }

        private void SetDialogFilter()
        {
            _saveFileDialog.Filter = "Xml file|*.xml";
            _saveFileDialog.DefaultExt = ".xml";
        }
        private void GetXmlFileName()
        {
            _states.XmlFileName = _saveFileDialog.ShowDialog() == true ? _saveFileDialog.FileName : null;
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

        private void CreateXmlFile()
        {
            var doc = new XDocument(new XElement("Peoples"));
            doc.Save(_states.XmlFileName);
        }

        private void FillRoot(AppDbContext dataBase)
        {
            var doc = XDocument.Load(_states.XmlFileName);

            foreach (var batch in PagenationList(dataBase))
            {
                foreach (var person in batch)
                {
                    var fg = new XElement("Person",
                        new XElement("Date", $"{person.Date}"),
                        new XElement("Name", $"{person.Name}"),
                        new XElement("Surname", $"{person.Surname}"),
                        new XElement("Patronymic", $"{person.Patronymic}"),
                        new XElement("City", $"{person.City}"),
                        new XElement("Country", $"{person.Country}"));
                    doc.Root.Add(fg);
                }
            }

            doc.Save(_states.XmlFileName);
        }

        public void FillXmlFile(AppDbContext dataBase)
        {
            SetDialogFilter();
            GetXmlFileName();
            CreateXmlFile();
            FillRoot(dataBase);
        }
    }
}
