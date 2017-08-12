using HappyHours.Dal.Dataset;
using HappyHours.Logic.BL;
using HappyHours.Models.Signup;
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
    public class SignupController : BaseApiController
    {
        private SignupBL BL = new SignupBL();

        [HttpPost]
        public SignupResponse Index(SignupRequest request)
        {
            return BL.Signup(request, db);
        }
    }
}
