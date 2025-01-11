
using FluentValidation;
using PurchaseOrderService.Application.Commands.CreatePurchaseOrder;

namespace PurchaseOrderService.Application.Validators;

public class AddPurchaseOrderItemCommandValidator : AbstractValidator<PurchaseOrderItemDto>
{
    public AddPurchaseOrderItemCommandValidator()
    {
        RuleFor(x => x.GoodCode)
            .NotEmpty().WithMessage("Good code is required.")
            .Length(3, 50).WithMessage("Good code must be between 3 and 50 characters.");

        RuleFor(x => x.Price.Amount)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(x => x.Price.Currency)
            .NotEmpty().WithMessage("Currency is required.")
            .Length(3).WithMessage("Currency code must be 3 characters.");
    }
}
