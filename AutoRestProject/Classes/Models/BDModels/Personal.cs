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
        public string first_name { get; set; }
        public string second_name { get; set; }
        public string patronymic { get; set; }
        public int pin { get; set; }
        public string phone_number { get; set; }

        //public List<Order> Orders { get; set; }
        public int positionId { get; set; }


        public override string ToString() => id + ": " + first_name;
    }
}
