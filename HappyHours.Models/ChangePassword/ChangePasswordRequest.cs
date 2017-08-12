using HappyHours.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Models.ChangePassword
{
    public class ChangePasswordRequest : BaseRequest
    {
        public string Password { get; set; }

        public string NewPassword { get; set; }
    }
}
