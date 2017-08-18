using HappyHours.Logic.Helpers;
using HappyHours.Web.Attributes;
using HappyHours.Web.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Routing;
using System.Web.SessionState;

namespace HappyHours.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static void RegisterWebApiFilters(HttpFilterCollection filters)
        {
            filters.Add(new ModelStateAttribute(1));
            filters.Add(new ErrorHandlingAttribute(3));
        }

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RegisterWebApiFilters(GlobalConfiguration.Configuration.Filters);

            // Start clean by replacing with filter provider for global configuration.
            // For these globally added filters we need not do any ordering as filters are 
            // executed in the order they are added to the filter collection
            GlobalConfiguration.Configuration.Services.Replace(typeof(IFilterProvider), new System.Web.Http.Filters.ConfigurationFilterProvider());

            // Custom action filter provider which does ordering
            GlobalConfiguration.Configuration.Services.Add(typeof(IFilterProvider), new OrderedFilterProvider());

            Storage.Initialize();
        }
    }
}
