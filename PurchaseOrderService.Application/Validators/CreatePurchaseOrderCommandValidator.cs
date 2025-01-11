
using FluentValidation;
using PurchaseOrderService.Application.Commands.CreatePurchaseOrder;

namespace PurchaseOrderService.Application.Validators;

public class CreatePurchaseOrderCommandValidator : AbstractValidator<CreatePurchaseOrderCommand>
{
    public CreatePurchaseOrderCommandValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty().WithMessage("Purchase order number is required.")
            .Length(5, 20).WithMessage("Purchase order number must be between 5 and 20 characters.");

        RuleFor(x => x.Date)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Purchase order date cannot be in the future.");
    }
}
