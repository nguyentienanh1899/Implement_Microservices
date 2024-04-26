using Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordering.Domain.Enums;
using Contracts.Common.Events;
using Ordering.Domain.OrderAggregate.Events;

namespace Ordering.Domain.Entities
{
    public class Order : AuditableEventEntity<long>
    {
        public Guid DocumentNo { get; set; } = Guid.NewGuid();
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string UserName { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string FirstName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Column(TypeName = "nvarchar(250)")]
        public string EmailAddress { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string ShippingAddress { get; set; }

        [Column(TypeName = "nvarchar(max)")]
        public string InvoiceAddress { get; set; }

        public OrderStatus Status { get; set; }

        public Order AddedOrder()
        {
            AddDomainEvent(new OrderCreatedEvent(Id, UserName, DocumentNo.ToString(), TotalPrice, EmailAddress, InvoiceAddress, ShippingAddress));
            return this;
        }

        public Order DeletedOrder()
        {
            AddDomainEvent(new OrderDeletedEvent(Id));
            return this;
        }
    }
}
