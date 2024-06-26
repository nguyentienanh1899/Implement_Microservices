﻿using Contracts.Common.Events;

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
        public string FullName { get; set; }

        public OrderCreatedEvent(long id, string userName, string documentNo, decimal totalPrice, string emailAddress, string invoiceAddress, string shippingAddress, string fullName)
        {
            id = Id;
            userName = UserName;
            documentNo = DocumentNo;
            totalPrice = TotalPrice;
            emailAddress = EmailAddress;
            invoiceAddress = InvoiceAddress;
            shippingAddress = ShippingAddress;
            fullName = FullName;
        }
    }
}
