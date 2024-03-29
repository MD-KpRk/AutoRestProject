﻿using System.ComponentModel.DataAnnotations.Schema;

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
        public Order_string_status Order_string_status { get; set; }
        public int Order_String_StatusId { get; set; }
        public int? CookPersId { get; set; }

        public Personal? CookPers { get; set; }

        [NotMapped]
        public string Price { get; set; } = "";
    }
}
