using Auto.BizLogic.Models.Dto;
using Auto.Data.Repositories;
using FluentValidation;

namespace Auto.BizLogic.Validators
{
    public class CreateAutoDtoValidator : AbstractValidator<CreateAutoDto>
    {
        private readonly IUserRepository _userRepository;

        public CreateAutoDtoValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Make)
                .NotEmpty().WithMessage("Make is required.")
                .NotNull().WithMessage("Make cannot be null.");

            RuleFor(x => x.Model)
                .NotEmpty().WithMessage("Model is required.")
                .NotNull().WithMessage("Model cannot be null.");

            RuleFor(x => x.Condition)
                .NotEmpty().WithMessage("Condition is required.")
                .NotNull().WithMessage("Condition cannot be null.");

            RuleFor(x => x.ModelYear)
                .GreaterThan(1900).WithMessage("Model year must be greater than 1900.");

            RuleFor(x => x.Mileage)
                .GreaterThan(0).WithMessage("Mileage must be greater than zero.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .NotNull().WithMessage("Description cannot be null.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero.");

            RuleFor(x => x.UserId)
                .MustAsync(ValidateUserIdAsync).WithMessage("User does not exist.");
        }

        private async Task<bool> ValidateUserIdAsync(int userId, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return user != null;
        }
    }
}