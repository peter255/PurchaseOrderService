using MediatR;

namespace PurchaseOrderService.Application.Commands.DeactivatePurchaseOrder;

public record DeactivatePurchaseOrderCommand : IRequest<bool>
{
    private int id;

    public DeactivatePurchaseOrderCommand(int id)
    {
        PurchaseOrderId = id;
    }

    public int PurchaseOrderId { get; init; }
}

