using Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ordering.Domain.Enums;

namespace Ordering.Domain.Entities
{
    public class Order : EntityAuditBase<long>
    {
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
    }
}
