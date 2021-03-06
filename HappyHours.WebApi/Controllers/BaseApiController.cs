﻿using HappyHours.Dal.Dataset;
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        protected string GetUsername()
        {
            return Request.GetOwinContext().Authentication.User.Identity.Name;
        }

        protected long GetUserId()
        {
            return Convert.ToInt64(Request.GetOwinContext().Authentication.User.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
        }
    }
}
