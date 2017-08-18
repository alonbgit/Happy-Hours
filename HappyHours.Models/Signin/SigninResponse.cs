using HappyHours.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Models.Signin
{
    public class SigninResponse : BaseResponse
    {
        public int ExtraMinutes { get; set; }

        public int LackMinutes { get; set; }

        public IEnumerable<DayTimeDetails> Days { get; set; }

        public string UserDisplayName { get; set; }

        public long UserId { get; set; } 
    }
}
