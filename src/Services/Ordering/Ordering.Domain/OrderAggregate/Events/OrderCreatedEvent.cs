using Contracts.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.OrderAggregate.Events
{
    public class OrderCreatedEvent : BaseEvent
    {
        public long Id { get; set; }
        public string UserName { get; private set; }
        public string DocumentNo { get; private set; }
        public string EmailAddress { get; private set; }
        public decimal TotalPrice { get; private set; }
        public string InvoiceAddress { get; private set; }
        public string ShippingAddress { get; private set; }

        public OrderCreatedEvent(long id, string userName, string documentNo, decimal totalPrice, string emailAddress, string invoiceAddress, string shippingAddress)
        {
            id = Id;
            userName = UserName;
            documentNo = DocumentNo;
            totalPrice = TotalPrice;
            emailAddress = EmailAddress;
            invoiceAddress = InvoiceAddress;
            shippingAddress = ShippingAddress;
        }
    }
}
