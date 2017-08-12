using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace HappyHours.Web.Helpers
{
    public class SessionManager
    {
        public static void Set(string key, object value)
        {
            Session[key] = value;
        }

        public static T Get<T>(string key)
        {
            return (T)Session[key];
        }

        public static HttpSessionState Session
        {
            get { return HttpContext.Current.Session;  }
        }
    }
}