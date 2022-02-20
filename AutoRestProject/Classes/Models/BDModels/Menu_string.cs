using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRestProject.Classes.Models.BDModels
{
    public class Menu_string
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }
    }
}
