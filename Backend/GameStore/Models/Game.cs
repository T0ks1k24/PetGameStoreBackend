namespace GameStore.Models;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price {  get; set; }
    public double Evaluation { get; set; }
    public string UrlImage { get; set; }
    public int CategoryId {  get; set; }

    public Category Category { get; set; }
    public ICollection<GameKey> GameKeys { get; set; } = new List<GameKey>();
}
