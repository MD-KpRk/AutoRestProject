using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRestProject.Classes.Models.BDModels
{
    public class Order_string
    {
        public int ID { get; set; }
        public int Food_count { get; set; }
        public Food Food { get; set; }
        public int FoodId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int OrderStringStatusId { get; set; }
        public OrderStringStatus OrderStringStatus { get; set; }

    }
}
