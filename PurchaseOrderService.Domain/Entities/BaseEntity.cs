using System.ComponentModel.DataAnnotations;

namespace PurchaseOrderService.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        //public DateTime CreatedAt { get; set; }
        //public DateTime UpdatedAt { get; set; } = DateTime.Now;
        //public DateTime? DeletedAt { get; set; }
        //public bool IsDeleted { get; set; } = false;
    }
}
