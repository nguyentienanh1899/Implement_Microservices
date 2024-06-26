﻿using Shared.SeedWork;

namespace Shared.DTOs.Inventory
{
    public class InventoryPagingQuery : PagingRequestParameters
    {
        public string _itemNo;
        public string ItemNo() => _itemNo;

        public void SetItemNo(string itemNo) => _itemNo = itemNo;
        public string? SearchTerm { get; set; }

    }
}
