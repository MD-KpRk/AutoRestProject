using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
