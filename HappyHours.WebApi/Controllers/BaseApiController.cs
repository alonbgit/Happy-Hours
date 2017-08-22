using HappyHours.Dal.Dataset;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HappyHours.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        protected dbDataContext db = new dbDataContext();
        private ApplicationUserManager _userManager;

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
