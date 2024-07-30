using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Order
{
    public class CreateOrderDto
    {
        public decimal TotalPrice { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string ShippingAddress { get; set; }
        public string invoiceAddress;
        public string? InvoiceAddress 
        {
            get => invoiceAddress;
            set => invoiceAddress = value ?? ShippingAddress;
        }


    }
}
