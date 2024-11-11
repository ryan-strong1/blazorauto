namespace Auto.BizLogic.Models.Dto
{
    public class CreateOfferDto
    {
        public int AutoId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
    }
}