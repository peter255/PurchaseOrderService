using PurchaseOrderService.Domain.Enums;
using PurchaseOrderService.Domain.ValueObjects;

namespace PurchaseOrderService.Domain.Entities
{
    public class PurchaseOrder : BaseEntity
    {
        public string PONumber { get; private set; }
        public DateTime PODate { get; private set; }
        public Money TotalPrice { get; private set; }
        public bool IsActive { get; set; } = true;
        public PurchaseOrderState State { get; private set; } = PurchaseOrderState.Created;

        private readonly List<PurchaseOrderItem> _items = new();
        public IReadOnlyCollection<PurchaseOrderItem> Items => _items.AsReadOnly();

        private PurchaseOrder() { } // Required for EF Core

        public PurchaseOrder(string number, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("Purchase Order number cannot be empty.", nameof(number));

            PONumber = number;
            PODate = date;
            TotalPrice = new Money(0, "USD"); // Default currency
        }

        public void AddItem(PurchaseOrderItem item)
        {
            if (!IsActive)
                throw new InvalidOperationException("Cannot add items to a deactivated purchase order.");

            if (State == PurchaseOrderState.Closed)
                throw new InvalidOperationException("Cannot add items to a closed purchase order.");

            if (_items.Any(i => i.GoodCode == item.GoodCode))
                throw new InvalidOperationException($"Item with code {item.GoodCode} already exists.");

            _items.Add(item);
            UpdateTotalPrice();
        }

        public void RemoveItem(string goodCode)
        {
            if (!IsActive)
                throw new InvalidOperationException("Cannot remove items from a deactivated purchase order.");

            if (State == PurchaseOrderState.Closed)
                throw new InvalidOperationException("Cannot remove items from a closed purchase order.");

            var item = _items.FirstOrDefault(i => i.GoodCode == goodCode);
            if (item == null)
                throw new InvalidOperationException($"Item with code {goodCode} not found.");

            _items.Remove(item);
            UpdateTotalPrice();
        }

        public void ChangeState(PurchaseOrderState newState)
        {
            if (!IsActive)
                throw new InvalidOperationException("Cannot change state of a deactivated purchase order.");

            if (newState < State)
                throw new InvalidOperationException("Cannot revert to a previous state.");

            State = newState;
        }

        public void Deactivate()
        {
            if (!IsActive)
                throw new InvalidOperationException("Purchase order is already deactivated.");

            IsActive = false;
        }


        private void UpdateTotalPrice()
        {
            var totalAmount = _items.Sum(i => i.Price.Amount);
            TotalPrice = new Money(totalAmount, "USD");
        }
    }


}
