using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseOrderService.Domain.Enums
{
    public enum PurchaseOrderState
    {
        Created,
        Approved,
        Shipped,
        Closed
    }
}
