using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.Emails
{
    public class GmailSendEmailParameters
    {
        public string FromDisplayName { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
