using HappyHours.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Models.Signup
{
    public class CheckTACredentialsResponse : BaseResponse
    {
        public bool Valid { get; set; }
    }
}
