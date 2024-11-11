using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auto.Data.Entities
{
    public class AutoEntity
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

        public Point Location { get; set; }

        public int UserId { get; set; }

        public UserEntity User { get; set; }

        public ICollection<OfferEntity> Offers { get; set; } = new List<OfferEntity>();
    }
}