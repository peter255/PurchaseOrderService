

using MediatR;
using PurchaseOrderService.Domain.Interfaces;


namespace PurchaseOrderService.Application.Commands.DeactivatePurchaseOrder
{
    public class DeactivatePurchaseOrderHandler : IRequestHandler<DeactivatePurchaseOrderCommand, bool>
    {
        private readonly IPurchaseOrderRepository _repository;

        public DeactivatePurchaseOrderHandler(IPurchaseOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeactivatePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var purchaseOrder = await _repository.GetByIdAsync(request.PurchaseOrderId);

            if (purchaseOrder == null)
                throw new InvalidOperationException($"Purchase order with ID {request.PurchaseOrderId} not found.");

            purchaseOrder.Deactivate();

            var result = await _repository.SaveChangesAsync();

            return result;
        }
    }
}