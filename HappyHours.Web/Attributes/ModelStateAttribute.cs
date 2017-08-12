using HappyHours.Web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using HappyHours.Models.Common;
using System.Net.Http;

namespace HappyHours.Web.Attributes
{
    public class ModelStateAttribute : ActionFilterAttribute, IOrderedFilter
    {
        public ModelStateAttribute(int order)
        {
            this.Order = order;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var errorResponse = new BaseResponse()
                {
                    ErrorCode = ErrorCode.BadRequest
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