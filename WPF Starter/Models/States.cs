using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_Starter.Models
{
    public class States
    {
        public LinkedList<People> peoples = new();
        public List<People> AllSov = new();
        public string? File { get; set; }
        public string[]? TextFromSeacrhBox { get; set; }

    }
}
