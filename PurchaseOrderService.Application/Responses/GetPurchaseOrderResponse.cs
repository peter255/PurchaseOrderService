using PurchaseOrderService.Domain.Enums;

namespace PurchaseOrderService.Application.Responses
{
    public class GetPurchaseOrderResponse
    {        
        public int Id { get; set; }
        public string PONumber { get; set; }
        public DateTime PODate { get; set; }
        public PurchaseOrderState State { get; set; }
        public string TotalPrice { get; set; }
        public bool IsActive { get; set; }
        public List<GetPurchaseOrderItemResponse> Items { get; set; }
    }

    public class GetPurchaseOrderItemResponse
    {
        public int Id { get; set; }
        public int SerialNumber { get; set; }
        public string GoodCode { get; set; }
        public string Price { get; set; }
    }
}
