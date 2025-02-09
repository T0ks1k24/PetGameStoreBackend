namespace GameStore.Dtos.Game;

public class CreateGameDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; }= string.Empty;
    public decimal Price { get; set; }
    public double Evaluation { get; set; }
    public string UrlImage { get; set; }
    public int CategoryId { get; set; }
}
