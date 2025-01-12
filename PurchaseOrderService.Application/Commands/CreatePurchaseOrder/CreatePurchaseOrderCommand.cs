using MediatR;
using PurchaseOrderService.Domain.ValueObjects;

namespace PurchaseOrderService.Application.Commands.CreatePurchaseOrder;

public record PurchaseOrderItemDto(string GoodCode, Money Price);

public record CreatePurchaseOrderCommand(string Number, DateTime Date,  List<PurchaseOrderItemDto> Items) : IRequest<int>;

