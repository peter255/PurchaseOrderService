using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PurchaseOrderService.Domain.ValueObjects;

namespace PurchaseOrderService.Domain.Entities
{
    public class PurchaseOrderItem : BaseEntity
    {
        public int POId { get; set; }

        [ForeignKey(nameof(POId))]
        public PurchaseOrder PO { get; set; }

        public int SerialNumber { get; set; }

        public string GoodCode { get; set; } // Unique for each PO
        
        public Money Price { get; set; }

        // Default constructor for EF Core
        private PurchaseOrderItem() { }

        public PurchaseOrderItem(string goodCode, Money price)
        {
            
            if (string.IsNullOrWhiteSpace(goodCode))
                throw new ArgumentException("Good code cannot be empty.", nameof(goodCode));
            GoodCode = goodCode;
            Price = price;
        }
    }
}
