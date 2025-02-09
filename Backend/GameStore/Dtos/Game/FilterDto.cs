namespace GameStore.Dtos.Game
{
    public class FilterDto
    {
        public string? Name { get; set; }      
        public decimal? MinPrice { get; set; }  
        public decimal? MaxPrice { get; set; }  
        public double? Evaluation { get; set; }  
        public int? CategoryId { get; set; } 
    }
}
