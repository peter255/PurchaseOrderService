using MediatR;
using PurchaseOrderService.Domain.Interfaces;
using PurchaseOrderService.Domain.Entities;
using PurchaseOrderService.Domain.ValueObjects;

namespace PurchaseOrderService.Application.Commands.CreatePurchaseOrder
{

    public class CreatePurchaseOrderHandler : IRequestHandler<CreatePurchaseOrderCommand, int>
    {
        private readonly IPurchaseOrderRepository _repository;

        public CreatePurchaseOrderHandler(IPurchaseOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreatePurchaseOrderCommand request, CancellationToken cancellationToken)
        {
            var purchaseOrder = new PurchaseOrder(request.Number, request.Date);

            // Add Items
            foreach (var item in request.Items)
            {
                var orderItem = new PurchaseOrderItem(item.GoodCode, item.Price);
                purchaseOrder.AddItem(orderItem);
            }


            await _repository.AddAsync(purchaseOrder);
            await _repository.SaveChangesAsync();
            return purchaseOrder.Id;
        }
    }
}
