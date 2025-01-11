using MediatR;
using PurchaseOrderService.Domain.Interfaces;
using PurchaseOrderService.Domain.Entities;

namespace PurchaseOrderService.Application.Queries.GetPurchaseOrder
{

    public class GetPurchaseOrderHandler : IRequestHandler<GetPurchaseOrderQuery, PurchaseOrder>
    {
        private readonly IPurchaseOrderRepository _repository;

        public GetPurchaseOrderHandler(IPurchaseOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<PurchaseOrder> Handle(GetPurchaseOrderQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByIdAsync(request.Id);
        }
    }
}