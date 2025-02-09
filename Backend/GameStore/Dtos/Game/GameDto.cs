namespace GameStore.Dtos.Game;

public class GameDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public double Evaluation { get; set; }
    public string UrlImage { get; set; }
    public string CategoryName { get; set; }
}
