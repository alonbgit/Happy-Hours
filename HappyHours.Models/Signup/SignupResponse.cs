using HappyHours.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Models.Signup
{
    public class SignupResponse : BaseResponse
    {
        public int ExtraMinutes { get; set; }

        public int LackMinutes { get; set; }

        public IEnumerable<DayTimeDetails> Days { get; set; }

        public string User { get; set; }

        public bool IsEmailVerificationRequired { get; set; }
    }
}
