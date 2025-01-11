using PurchaseOrderService.Domain.Entities;

namespace PurchaseOrderService.Domain.Interfaces
{
    public interface IPurchaseOrderRepository
    {
        Task<PurchaseOrder> GetByIdAsync(int id);
        Task AddAsync(PurchaseOrder purchaseOrder);
        Task<bool> SaveChangesAsync();
    }
}
