using System.ComponentModel.DataAnnotations.Schema;

namespace WPF_Starter.Models
{
    [Table("Table")]
    public class People
    {
        public int Id { get; set; }
        public DateTime? Date {  get; set; }
        public string? Name {  get; set; }
        public string? Surname {  get; set; }
        public string? Patronymic {  get; set; }
        public string? City {  get; set; }
        public string? Country {  get; set; }
    }
}
