﻿using HappyHours.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Models.Signup
{
    public class CheckEmailExistResponse : BaseResponse
    {
        public bool Exist { get; set; }
    }
}