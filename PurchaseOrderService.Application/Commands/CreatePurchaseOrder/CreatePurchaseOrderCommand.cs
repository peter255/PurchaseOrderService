using MediatR;
using PurchaseOrderService.Domain.ValueObjects;

namespace PurchaseOrderService.Application.Commands.CreatePurchaseOrder;

public record PurchaseOrderItemDto(string GoodCode, Money Price);

public record CreatePurchaseOrderCommand(string Number, DateTime Date, decimal TotalPrice, List<PurchaseOrderItemDto> Items) : IRequest<int>;

