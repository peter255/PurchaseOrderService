using MediatR;
using PurchaseOrderService.Domain.Interfaces;
using PurchaseOrderService.Domain.Entities;
using PurchaseOrderService.Application.Responses;

namespace PurchaseOrderService.Application.Queries.GetPurchaseOrder
{

    public class GetPurchaseOrderHandler : IRequestHandler<GetPurchaseOrderQuery, GetPurchaseOrderResponse>
    {
        private readonly IPurchaseOrderRepository _repository;

        public GetPurchaseOrderHandler(IPurchaseOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetPurchaseOrderResponse> Handle(GetPurchaseOrderQuery request, CancellationToken cancellationToken)
        {
            var obj = await _repository.GetByIdAsync(request.Id);

            return new GetPurchaseOrderResponse
            {
                Id = obj.Id,
                PONumber = obj.PONumber,
                PODate = obj.PODate,
                State = obj.State,
                TotalPrice = obj.TotalPrice.ToString(),
                IsActive = obj.IsActive,
                Items= obj.Items.Select(s => new GetPurchaseOrderItemResponse
                {
                    Id = s.Id,
                    SerialNumber = s.SerialNumber,
                    GoodCode= s.GoodCode,
                    Price = s.Price.ToString()
                }).ToList()
            };
        }
    }
}