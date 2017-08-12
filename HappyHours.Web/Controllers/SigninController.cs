﻿using HappyHours.Logic.BL;
using HappyHours.Logic.Helpers;
using HappyHours.Models.Signin;
using HappyHours.Web.Attributes;
using HappyHours.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HappyHours.Web.Controllers
{
    [Authenticate(2)]
    public class SigninController : BaseApiController
    {
        private SigninBL BL = new SigninBL();

        [HttpPost]
        public SigninResponse Index(SigninRequest request)
        {
            return BL.Signin(request, db);
        }
    }
}