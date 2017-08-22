using HappyHours.Dal.Dataset;
using HappyHours.Logic.BL;
using HappyHours.Models.Signup;
using HappyHours.WebApi.Attributes;
using HappyHours.WebApi.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HappyHours.WebApi.Controllers
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

        [HttpPost]
        public CheckEmailExistResponse CheckEmailExist(CheckEmailExistRequest request)
        {
            return BL.CheckEmailExist(request, db);
        }

        [HttpPost]
        public CheckTACredentialsResponse CheckTACredentials(CheckTACredentialsRequest request)
        {
            return BL.CheckTACredentials(request, db);
        }
    }
}
