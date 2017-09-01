using HappyHours.Dal.Dataset;
using HappyHours.Logic.Core;
using HappyHours.Logic.Helpers;
using HappyHours.Models.Common;
using HappyHours.Models.Signin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.BL
{
    public class SigninBL
    {
        public BaseResponse Signin(SigninRequest request, dbDataContext db)
        {
            var md5Password = Md5SecurityHelper.GetMd5Hash(request.Password);

            var result = db.sp_Signin(request.Email, md5Password).FirstOrDefault();

            if (result == null)
                throw new HappyHourException(ErrorCode.InvalidUser);

            if (ConfigHelper.Config.SignupActivationRequired)
            {
                if (!result.IsEmailVerified)
                    throw new HappyHourException(ErrorCode.EmailNotVerified);
            }

            return new BaseResponse();
        }
    }
}
