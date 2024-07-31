﻿using Shared.Enums.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Inventory
{
    public record SalesProductDto(string ExternalDocumentNo, int quantity)
    {
        public DocumentType DocumentType = DocumentType.Sale;
        public string ItemNo {  get; set; }
        public void SetItemNo (string itemNo)
        {
            ItemNo = itemNo;
        }
    }
}
