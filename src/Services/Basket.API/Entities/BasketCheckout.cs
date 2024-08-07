﻿namespace Basket.API.Entities
{
    public class BasketCheckout
    {
        public string UserName { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string EmailAddress { get; set; } = string.Empty;
        public string _invoiceAddress;
        public string? InvoiceAddress 
        { 
            get => _invoiceAddress;
            set => _invoiceAddress = value ?? ShippingAddress;
        }
        public string ShippingAddress { get; set; }
    }
}
