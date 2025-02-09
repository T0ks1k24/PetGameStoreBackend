namespace GameStore.Dtos.Game
{
    public class ViewGameDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public double Evaluation { get; set; }
        public string UrlImage { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
