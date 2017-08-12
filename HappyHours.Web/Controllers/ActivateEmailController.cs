using HappyHours.Logic.BL;
using HappyHours.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HappyHours.Web.Controllers
{
    public class ActivateEmailController : BaseApiController
    {
        private ActivateEmailBL BL = new ActivateEmailBL();

        [HttpGet]
        public HttpResponseMessage Index(string token)
        {
            BL.ActivateEmail(token, db);

            var response = Request.CreateResponse(HttpStatusCode.Moved);
            response.Headers.Location = new Uri("http://google.com");
            return response;
        }
    }
}
