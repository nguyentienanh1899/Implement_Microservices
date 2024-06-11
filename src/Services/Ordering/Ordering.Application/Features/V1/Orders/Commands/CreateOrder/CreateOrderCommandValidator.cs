using FluentValidation;

namespace Ordering.Application.Features.V1.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("{UserName} is required.").NotNull().MaximumLength(50).WithMessage("{UserName} must not exceed 50 characters");
        }
    }
}
