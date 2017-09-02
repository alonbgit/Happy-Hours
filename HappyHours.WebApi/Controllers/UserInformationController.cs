using HappyHours.Logic.BL;
using HappyHours.Models.Common;
using HappyHours.Models.UserInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace HappyHours.WebApi.Controllers
{
    [Authorize]
    public class UserInformationController : BaseApiController
    {
        public UserInformationBL BL = new UserInformationBL();

        [HttpPost]
        public UserInformationResponse Index(UserInformationRequest request)
        {
            var userId = GetUserId();
            return BL.GetUserInformation(request, userId, db);
        }
    }
}
