﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Enums.Order
{
    public enum OrderStatus
    {
        New = 1, // start with 1, 0 is used for filter all = 0
        Peding, //order peding, not activities for period time
        Paid,   //order is paid
        Shipping, //order is on the shipping
        Fulfilled, //order is fulfilled
    }
}
