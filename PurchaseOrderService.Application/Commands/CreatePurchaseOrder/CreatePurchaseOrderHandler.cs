using MediatR;
using PurchaseOrderService.Domain.Interfaces;
using PurchaseOrderService.Domain.Entities; 
namespace PurchaseOrderService.Application.Commands.CreatePurchaseOrder
{

    public class CreatePurchaseOrderHandler : IRequestHandler<CreatePurchaseOrderCommand, int>
    {
        private readonly IPurchaseOrderRepository _repository;
        private readonly IMessageProducer _messagePublisher;
        public CreatePurchaseOrderHandler(IPurchaseOrderRepository repository, IMessageProducer messagePublisher)
        {
            _repository = repository;
            _messagePublisher = messagePublisher;
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

            await _messagePublisher.SendMessageAsync(new CreateShippingOrderDto(
                poId: purchaseOrder.Id,
                ShippingDate: purchaseOrder.PODate,
                TrackingNumber: purchaseOrder.PONumber.ToString()
            ));
            return purchaseOrder.Id;
        }
    }
}
