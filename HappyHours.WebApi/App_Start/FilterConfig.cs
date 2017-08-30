using HappyHours.WebApi.Attributes;
using System.Web;
using System.Web.Mvc;

namespace HappyHours.WebApi
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
