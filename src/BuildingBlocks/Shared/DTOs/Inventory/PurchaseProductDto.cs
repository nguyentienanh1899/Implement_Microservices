﻿using Shared.Enums.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Inventory
{
    public class PurchaseProductDto
    {
        public string ItemNo {  get; set; }
        public string DocumentNo {  get; set; }
        public decimal Quantity {  get; set; }
        public string ExternalDocumentNo { get; set; }
        public DocumentType DocumentType => DocumentType.Purchase;
    }
}