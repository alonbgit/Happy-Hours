using HappyHours.Dal.Dataset;
using HappyHours.Logic.Core;
using HappyHours.Logic.Emails;
using HappyHours.Logic.Helpers;
using HappyHours.Logic.Helpers.Emails;
using HappyHours.Models.Common;
using HappyHours.Models.Signup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.BL
{
    public class SignupBL
    {
        public SignupResponse Signup(SignupRequest request, dbDataContext db)
        {
            var result = db.sp_GetUserStatusByEmail(request.Email).FirstOrDefault();

            if (result != null)
            {
                if (!result.IsEmailVerified == true)
                    throw new HappyHourException(ErrorCode.EmailNotVerified);

                throw new HappyHourException(ErrorCode.EmailAlreadyExist);
            }

            var today = DateTime.Today;

            var loginParameters = new HappyHoursLoginParameters()
            {
                Credentials = new HappyHoursCredentials()
                {
                    Username = request.SystemEmail,
                    Password = request.SystemPassword,
                    Number = request.SystemNumber
                },
                StartDate = new DateTime(today.Year, today.Month, 1),
                EndDate = today
            };

            HappyHoursCoreBL manager = new HappyHoursCoreBL();
            HappyHourSummary summaryResult = manager.GetSummary(loginParameters);

            var md5Password = Md5SecurityHelper.GetMd5Hash(request.Password);

            var encyptedSystemPassword = PasswordEncryptor.Encrypt(request.SystemPassword);
            var encryptedSystemNumber = PasswordEncryptor.Encrypt(request.SystemNumber);

            var signupResult = db.sp_Signup(request.Email, md5Password, request.SystemEmail, encyptedSystemPassword, encryptedSystemNumber).FirstOrDefault();

            if (signupResult.EmailExist == true)
                throw new HappyHourException(ErrorCode.EmailAlreadyExist);

            var activationLink = CreateActivationLink(signupResult.ActivationToken.Value.ToString());

            SendActivationEmail(request.Email, summaryResult.User, activationLink);

            return new SignupResponse()
            {
                User = summaryResult.User,
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

        public CheckEmailExistResponse CheckEmailExist(CheckEmailExistRequest request, dbDataContext db)
        {
            var result = db.sp_GetUserStatusByEmail(request.Email).FirstOrDefault();

            if (result != null)
            {
                return new CheckEmailExistResponse()
                {
                    Exist = true
                };
            }

            return new CheckEmailExistResponse()
            {
                Exist = false
            };
        }

        private string CreateActivationLink(string activationLink)
        {
            var url = string.Format("http://{0}/api/ActivateEmail?Token={1}", ConfigHelper.Config.DomainName, activationLink);
            return url;
        }

        private void SendActivationEmail(string email, string user, string activationLink)
        {
            GMailEmailSender.SendEmail(new GmailSendEmailParameters()
            {
                From = ConfigHelper.Config.SMTP.From,
                FromDisplayName = ConfigHelper.Config.SMTP.FromDisplayName,
                Subject = "Welcome to Happy Hours!",
                To = email,
                Body = EmailTemplateManager.CreateActivationEmailBody(new ActivationEmailParameters()
                {
                    User = user,
                    ActivationLink = activationLink
                })
            });
        }
    }
}
