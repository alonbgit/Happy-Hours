﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace HappyHours.Web.Helpers
{
    public interface IOrderedFilter : IFilter
    {
        int Order { get; set; }
    }
}