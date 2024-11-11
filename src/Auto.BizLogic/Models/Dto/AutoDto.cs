namespace Auto.BizLogic.Models.Dto
{
    public class AutoDto
    {
        public int AutoId { get; set; }
        public string Make { get; set; }

        public string Model { get; set; }

        public string Condition { get; set; }

        public int ModelYear { get; set; }

        public int Mileage { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }

        public int UserId { get; set; }
    }
}