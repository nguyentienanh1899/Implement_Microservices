using Shared.Enums.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Inventory
{
    public class SaleItemDto
    {
        public string ItemNo {  get; set; }
        public int Quantity { get; set; }
        public DocumentType DocumentType => DocumentType.Sale;
    }
}
