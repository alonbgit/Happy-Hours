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
                if (ConfigHelper.Config.SignupActivationRequired)
                {
                    if (!result.IsEmailVerified == true)
                        throw new HappyHourException(ErrorCode.EmailNotVerified);
                }

                throw new HappyHourException(ErrorCode.EmailAlreadyExist);
            }

            var md5Password = Md5SecurityHelper.GetMd5Hash(request.Password);

            var encyptedSystemPassword = PasswordEncryptor.Encrypt(request.SystemPassword);
            var encryptedSystemNumber = PasswordEncryptor.Encrypt(request.SystemNumber);

            var signupResult = db.sp_Signup(request.FirstName, request.LastName, request.Email,
                md5Password, request.SystemEmail, encyptedSystemPassword, encryptedSystemNumber, ConfigHelper.Config.SignupActivationRequired).FirstOrDefault();

            if (signupResult.EmailExist == true)
                throw new HappyHourException(ErrorCode.EmailAlreadyExist);

            if (ConfigHelper.Config.SignupActivationRequired)
            {
                var activationLink = CreateActivationLink(signupResult.ActivationToken.Value.ToString());
                string fullName = string.Format("{0} {1}", request.FirstName, request.LastName);
                SendActivationEmail(request.Email, fullName, activationLink);
            }

            return new SignupResponse()
            {
                IsEmailVerificationRequired = ConfigHelper.Config.SignupActivationRequired
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

        public CheckTACredentialsResponse CheckTACredentials(CheckTACredentialsRequest request, dbDataContext db)
        {
            var today = DateTime.Today;
            var loginParameters = new HappyHoursLoginParameters()
            {
                Credentials = new HappyHoursCredentials()
                {
                    Username = request.TAEmail,
                    Password = request.TAPassword,
                    Number = request.TANumber
                },
                StartDate = new DateTime(today.Year, today.Month, 1),
                EndDate = today
            };

            HappyHoursCoreBL manager = new HappyHoursCoreBL();

            try
            {
                HappyHourSummary summaryResult = manager.GetSummary(loginParameters);
            }
            catch
            {
                return new CheckTACredentialsResponse()
                {
                    Valid = false
                };
            }

            return new CheckTACredentialsResponse()
            {
                Valid = true
            };
        }

        private string CreateActivationLink(string activationLink)
        {
            var url = string.Format("http://{0}/api/ActivateEmail?Token={1}", ConfigHelper.Config.DomainName, activationLink);
            return url;
        }

        private void SendActivationEmail(string email, string user, string activationLink)
        {
            try
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
            catch (Exception ex) { }
        }
    }
}
