using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        //public List<Order> Orders { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }


        public override string ToString() => id + ": " + First_name;
    }
}
