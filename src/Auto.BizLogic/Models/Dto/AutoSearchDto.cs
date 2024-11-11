namespace Auto.BizLogic.Models.Dto
{
    public class AutoSearchDto
    {
        public string? Make { get; set; }
        public string? Model { get; set; }
        public string? Condition { get; set; }
        public int? ModelYear { get; set; }
        public int? MinMileage { get; set; }
        public int? MaxMileage { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public string? Color { get; set; }
    }
}