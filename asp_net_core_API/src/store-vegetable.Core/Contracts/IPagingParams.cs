﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Core.Contracts
{
    public interface IPagingParams
    {

        int PageSize { get; set; }

        int PageNumber { get; set; }

        string SortColumn { get; set; }

        string SortOrder { get; set; }
    }
}
