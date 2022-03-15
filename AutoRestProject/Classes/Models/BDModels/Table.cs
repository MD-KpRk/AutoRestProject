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
