using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseOrderService.Domain.ValueObjects
{
    public class Money
    {
        public decimal Amount { get; private set; }

        [MaxLength(5)]
        public string Currency { get; private set; }

        private Money() { } // Required for EF Core

        public Money(decimal amount, string currency = "USD")
        {
            if (amount < 0) throw new ArgumentException("Amount cannot be negative.", nameof(amount));
            if (string.IsNullOrWhiteSpace(currency)) throw new ArgumentException("Currency is required.", nameof(currency));

            Amount = amount;
            Currency = currency;
        }

        public override string ToString() => $"{Amount} {Currency}";
    }
}
