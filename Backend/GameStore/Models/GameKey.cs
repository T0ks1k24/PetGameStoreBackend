namespace GameStore.Models
{
    public class GameKey
    {
        public int Id {  get; set; }
        public string Key {  get; set; } = Guid.NewGuid().ToString();
        public int GameId { get; set; }
        public bool IsUser { get; set; } = false;

        public Game Game {  get; set; }
    }
}
