using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Enums.Inventory
{
    public enum DocumentType
    {
        All = 0,
        Purchase = 101,
        PurchaseInternal = 102,
        Sale = 201,
        SaleInternal = 202,
    }
}
