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
        public long UserId { get; set; }
    }
}
