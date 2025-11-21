using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WPF_Starter.Models
{
    [Table("Table")]
    public class People
    {
        public int Id { get; set; }

        public DateOnly Date {  get; set; }
        public string? Name {  get; set; }
        public string? Surname {  get; set; }
        public string? Patronymic {  get; set; }
        public string? City {  get; set; }
        public string? Country {  get; set; }
    }
}
