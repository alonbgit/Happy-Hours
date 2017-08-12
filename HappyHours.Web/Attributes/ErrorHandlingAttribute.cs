using HappyHours.Logic.BL;
using HappyHours.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using HappyHours.Web.Helpers;

namespace HappyHours.Web.Attributes
{
    public class ErrorHandlingAttribute : ExceptionFilterAttribute, IOrderedFilter
    {
        public ErrorHandlingAttribute(int order)
        {
            this.Order = order;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var exception = actionExecutedContext.Exception as HappyHourException;

            var errorResponse = new BaseResponse()
            {
                ErrorCode = exception.ErrorCode
            };

            var response = actionExecutedContext.Request.CreateResponse<BaseResponse>(errorResponse);
            actionExecutedContext.Response = response;

            base.OnException(actionExecutedContext);
        }

        public int Order
        {
            get; set;
        }
    }
}