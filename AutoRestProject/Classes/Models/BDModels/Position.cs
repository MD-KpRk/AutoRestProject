using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRestProject.Classes.Models.BDModels
{
    public class Position
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public List<Personal> Personals { get; set; } = new List<Personal>();
        public override string ToString() => Title;
    }
}
