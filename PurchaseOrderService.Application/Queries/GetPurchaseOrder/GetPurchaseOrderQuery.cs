using MediatR;
using PurchaseOrderService.Domain.Entities;

namespace PurchaseOrderService.Application.Queries.GetPurchaseOrder;
public record GetPurchaseOrderQuery(int Id) : IRequest<PurchaseOrder>;
