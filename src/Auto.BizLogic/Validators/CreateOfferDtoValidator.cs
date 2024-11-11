using Auto.BizLogic.Models.Dto;
using Auto.Data.Repositories;
using FluentValidation;

namespace Auto.BizLogic.Validators
{
    public class CreateOfferDtoValidator : AbstractValidator<CreateOfferDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAutoRepository _autoRepository;

        public CreateOfferDtoValidator(IUserRepository userRepository, IAutoRepository autoRepository)
        {
            _userRepository = userRepository;
            _autoRepository = autoRepository;

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");

            RuleFor(x => x.UserId)
                .MustAsync(ValidateUserIdAsync).WithMessage("User does not exist.");

            RuleFor(x => x.AutoId)
                .MustAsync(ValidateAutoIdAsync).WithMessage("Auto does not exist.");
        }

        private async Task<bool> ValidateUserIdAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user != null;
        }

        private async Task<bool> ValidateAutoIdAsync(int autoId, CancellationToken cancellationToken)
        {
            var auto = await _autoRepository.GetByIdAsync(autoId);
            return auto != null;
        }
    }
}