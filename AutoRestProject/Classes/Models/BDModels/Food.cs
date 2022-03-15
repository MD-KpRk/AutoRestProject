namespace AutoRestProject.Classes.Models.BDModels
{
    public class Food
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public double Price { get; set; }
        public bool Is_cooking { get; set; }
    }
}
