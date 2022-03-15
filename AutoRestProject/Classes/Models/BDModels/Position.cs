using System.Collections.Generic;

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
