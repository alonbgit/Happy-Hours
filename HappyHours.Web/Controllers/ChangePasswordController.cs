using HappyHours.Logic.BL;
using HappyHours.Models.ChangePassword;
using HappyHours.Models.Common;
using HappyHours.Web.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HappyHours.Web.Controllers
{
    [Authenticate(2)]
    public class ChangePasswordController : BaseApiController
    {
        private ChangePasswordBL BL = new ChangePasswordBL();

        [HttpPost]
        public BaseResponse Index(ChangePasswordRequest request)
        {
            return BL.ChangePassword(request, db);
        }
    }
}
