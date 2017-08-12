using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.Configuration
{
    public class HappyHoursConfiguration
    {
        public ApiCredentials ApiCredentials { get; set; }

        public string ChromeRequestFile { get; set; }

        public string SecurityKey { get; set; }

        public SMTPConfiguration SMTP { get; set; }

        public EmailTemplates EmailTemplates { get; set; }

        public string DomainName { get; set; }

        public int UserArrivalSchedulerInterval { get; set; }
    }
}
