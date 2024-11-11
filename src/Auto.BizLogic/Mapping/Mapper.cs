using Auto.BizLogic.Models.Dto;
using Auto.Data.Entities;

namespace Auto.BizLogic.Mapping
{
    public static class Mapper
    {
        public static UserDto ToDto(this UserEntity user)
        {
            return new UserDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                City = user.City,
                State = user.State,
                ZipCode = user.ZipCode,
                Country = user.Country,
                Role = user.Role,
                Status = user.Status,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
            };
        }

        public static AutoDto ToDto(this AutoEntity auto)
        {
            return new AutoDto
            {
                AutoId = auto.AutoId,
                Make = auto.Make,
                Model = auto.Model,
                Condition = auto.Condition,
                ModelYear = auto.ModelYear,
                Mileage = auto.Mileage,
                Description = auto.Description,
                Color = auto.Color,
                Price = auto.Price,
                UserId = auto.AutoId
            };
        }

        public static OfferDto ToDto(this OfferEntity offer)
        {
            return new OfferDto
            {
                OfferId = offer.OfferId,
                AutoId = offer.AutoId,
                UserId = offer.UserId,
                Amount = offer.Amount,
                Status = offer.Status,
                Created = offer.Created,
                Expires = offer.Expires
            };
        }

        public static Data.Filters.AutoFilter ToAutoFilter(this AutoSearchDto autoSearchDto)
        {
            return new Data.Filters.AutoFilter
            {
                Make = autoSearchDto.Make,
                Model = autoSearchDto.Model,
                Condition = autoSearchDto.Condition,
                ModelYear = autoSearchDto.ModelYear,
                MinMileage = autoSearchDto.MinMileage,
                MaxMileage = autoSearchDto.MaxMileage,
                MinPrice = autoSearchDto.MinPrice,
                MaxPrice = autoSearchDto.MaxPrice,
                Color = autoSearchDto.Color,
            };
        }
    }
}