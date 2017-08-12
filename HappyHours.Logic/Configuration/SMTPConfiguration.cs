using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.Configuration
{
    public class SMTPConfiguration
    {
        public string From { get; set; }

        public string FromDisplayName { get; set; }

        public string Password { get; set; }
    }
}
