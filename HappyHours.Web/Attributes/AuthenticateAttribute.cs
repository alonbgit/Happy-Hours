using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using HappyHours.Models.Common;
using HappyHours.Logic.Helpers;
using HappyHours.Web.Helpers;

namespace HappyHours.Web.Attributes
{
    public class AuthenticateAttribute : ActionFilterAttribute, IOrderedFilter
    {
        public AuthenticateAttribute(int order)
        {
            this.Order = order;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var request = actionContext.ActionArguments["request"] as BaseRequest;

            var isValidCredentials =
                request.Credentials.Username == ConfigHelper.Config.ApiCredentials.APIUsername &&
                request.Credentials.Password == ConfigHelper.Config.ApiCredentials.APIPassword;

            if (!isValidCredentials)
            {
                var errorResponse = new BaseResponse()
                {
                    ErrorCode = ErrorCode.InvalidCredentials
                };
                var response = actionContext.Request.CreateResponse<BaseResponse>(errorResponse);
                actionContext.Response = response;
            }

            base.OnActionExecuting(actionContext);
        }

        public int Order
        {
            get; set;
        }
    }
}