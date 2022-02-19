using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRestProject.Classes.Models.BDModels
{
    public class Table
    {
        public int Id { get; set; }
        public int Seats { get; set; }
        public int Table_StatusID { get; set; }
        public Table_status Table_Status { get; set; }
    }
}
