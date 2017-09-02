using HappyHours.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Models.UserInformation
{
    public class UserInformationRequest : BaseRequest
    {
        public int? Month { get; set; }
    }
}
