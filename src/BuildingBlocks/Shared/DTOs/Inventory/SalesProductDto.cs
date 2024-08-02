using Shared.Enums.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.Inventory
{
    /// <summary>
    /// ExternalDocumentNo is Order Document No
    /// </summary>
    /// <param name="ExternalDocumentNo"></param>
    /// <param name="Quantity"></param>
    public record SalesProductDto(string ExternalDocumentNo, int Quantity)
    {
        public DocumentType DocumentType = DocumentType.Sale;
        public string ItemNo {  get; set; }
        public void SetItemNo (string itemNo)
        {
            ItemNo = itemNo;
        }
    }
}
