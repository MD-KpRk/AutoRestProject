﻿using System.Collections.Generic;

namespace AutoRestProject.Classes.Models.BDModels
{
    public class Order
    {
        public int Id { get; set; }
        public string Time { get; set; }
        public string Date { get; set; }
        public int Order_StatusId { get; set; }
        public Order_status Order_status { get; set; }
        public int PersonalId { get; set; }
        public Personal Personal { get; set; }
        public int TableId { get; set; }
        public Table Table { get; set; }
        public List<Order_string> Order_strings { get; set; } = new List<Order_string>();

    }
}
