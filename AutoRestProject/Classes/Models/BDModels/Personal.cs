using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRestProject.Classes.Models.BDModels
{
    [Table("personals")]
    public class Personal
    {
        public int id { get; set; }
        public string First_name { get; set; }
        public string Second_name { get; set; }
        public string Patronymic { get; set; }
        public int Pin { get; set; }
        public string Phone_number { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
        public override string ToString() => id + ": " + First_name;
    }
}
