namespace Auto.BizLogic.Models.Dto
{
    public class AutoDto
    {
        public int AutoId { get; set; }
        public required string Make { get; set; }

        public required string Model { get; set; }

        public required string Condition { get; set; }

        public int ModelYear { get; set; }

        public int Mileage { get; set; }

        public required string Description { get; set; }

        public required string Color { get; set; }

        public decimal Price { get; set; }

        public int UserId { get; set; }
    }
}