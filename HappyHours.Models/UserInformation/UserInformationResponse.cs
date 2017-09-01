using HappyHours.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Models.UserInformation
{
    public class UserInformationResponse : BaseResponse
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int ExtraMinutes { get; set; }

        public int LackMinutes { get; set; }

        public IEnumerable<DayTimeDetails> Days { get; set; }
    }
}
