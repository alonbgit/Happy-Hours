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
        public SigninResponse Signin(SigninRequest request, dbDataContext db)
        {
            var md5Password = Md5SecurityHelper.GetMd5Hash(request.Password);

            var result = db.sp_Signin(request.Email, md5Password).FirstOrDefault();

            if (result == null)
                throw new HappyHourException(ErrorCode.InvalidUser);

            if (!result.IsEmailVerified)
                throw new HappyHourException(ErrorCode.EmailNotVerified);

            var today = DateTime.Today;

            var decryptedSystemPassword = PasswordEncryptor.Decrypt(result.SystemPassword);
            var decryptedSystemNumber = PasswordEncryptor.Decrypt(result.SystemNumber);

            var loginParameters = new HappyHoursLoginParameters()
            {
                Credentials = new HappyHoursCredentials()
                {
                    Username = result.SystemEmail,
                    Password = decryptedSystemPassword,
                    Number = decryptedSystemNumber
                },
                StartDate = new DateTime(today.Year, today.Month, 1),
                EndDate = today
            };

            HappyHoursCoreBL manager = new HappyHoursCoreBL();
            HappyHourSummary summaryResult = manager.GetSummary(loginParameters);

            return new SigninResponse()
            {
                UserId = result.Id,
                UserDisplayName = summaryResult.User,
                ExtraMinutes = summaryResult.ExtraMinutes,
                LackMinutes = summaryResult.LackMinutes,
                Days = summaryResult.DayDetails.Select(c => new DayTimeDetails()
                {
                    ExtraMinutes = c.ExtraMinutes,
                    LackMinutes = c.LackMinutes,
                    Date = HappyHourTimestampProvider.GetDateTimestamp(c.Date),
                    StartTime = HappyHourTimestampProvider.GetDateTimeTimestamp(c.StartTime),
                    EndTime = HappyHourTimestampProvider.GetDateTimeTimestamp(c.EndTime)
                }).ToList()
            };
        }
    }
}
