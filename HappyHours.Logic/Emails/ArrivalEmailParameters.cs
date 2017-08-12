using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.Emails
{
    public class ArrivalEmailParameters : IEmailTemplateParameters
    {
        public string User { get; set; }

        public string Time { get; set; }
    }
}
