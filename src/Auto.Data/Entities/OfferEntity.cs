namespace Auto.Data.Entities
{
    public class OfferEntity
    {
        public int OfferId { get; set; }
        public int AutoId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }

        //pending, accepted, rejected
        public string Status { get; set; }

        public DateTime Created { get; set; }

        public DateTime Expires { get; set; }
    }
}