using System.ComponentModel.DataAnnotations.Schema;

namespace AutoRestProject.Classes.Models.BDModels
{
    public class Menu_string
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public Food Food { get; set; }

        [NotMapped]
        public int Count { get; set; }
    }
}
