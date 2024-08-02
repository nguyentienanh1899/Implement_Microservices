using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Basket
{
    public class PaymentOrderDto
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
