using HappyHours.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Models.Signup
{
    public class CheckTACredentialsRequest : BaseRequest
    {
        public string TAEmail { get; set; }

        public string TAPassword { get; set; }

        public string TANumber { get; set; }
    }
}
