using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto.BizLogic.Models.Dto
{
    public class CreateAutoDto
    {
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