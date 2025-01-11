
using Microsoft.EntityFrameworkCore;
using PurchaseOrderService.Domain.Entities;
using PurchaseOrderService.Domain.Interfaces;
using PurchaseOrderService.Infrastructure.Persistence;

namespace PurchaseOrderService.Infrastructure.Repositories
{
    public class PurchaseOrderRepository : IPurchaseOrderRepository
    {
        private readonly PurchaseOrderDbContext _context;

        public PurchaseOrderRepository(PurchaseOrderDbContext context)
        {
            _context = context;
        }

        public async Task<PurchaseOrder> GetByIdAsync(int id)
        {
            return await _context.PurchaseOrders.Include(po => po.Items).FirstOrDefaultAsync(po => po.Id == id);
        }

        public async Task AddAsync(PurchaseOrder purchaseOrder)
        {
            await _context.PurchaseOrders.AddAsync(purchaseOrder);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
