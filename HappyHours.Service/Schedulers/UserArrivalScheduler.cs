using HappyHours.Dal.Dataset;
using HappyHours.Logic.Core;
using HappyHours.Logic.Emails;
using HappyHours.Logic.Helpers;
using HappyHours.Logic.Helpers.Emails;
using HappyHours.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Service.Schedulers
{
    public class UserArrivalScheduler : BaseScheduler
    {
        public UserArrivalScheduler(int interval) : base(interval)
        {
        }

        protected override void Elapsed()
        {
            using (var db = new dbDataContext())
            {
                var users = GetAllUsers(db);
                CheckUsersArrivalTime(users, db);
            }
        }

        private void CheckUsersArrivalTime(IEnumerable<User> users, dbDataContext db)
        {
            foreach(var user in users)
            {
                try
                {
                    CheckUserArrivalTime(user, db);
                }
                catch(Exception ex)
                {
                    // in case of an error, log the error, and skip to the next user
                }
            }
        }

        private void CheckUserArrivalTime(User user, dbDataContext db)
        {
            var decryptedSystemPassword = PasswordEncryptor.Decrypt(user.SystemPassword);
            var decryptedSystemNumber = PasswordEncryptor.Decrypt(user.SystemNumber);

            var today = DateTime.Today;

            var loginParameters = new HappyHoursLoginParameters()
            {
                Credentials = new HappyHoursCredentials()
                {
                    Username = user.SystemEmail,
                    Password = decryptedSystemPassword,
                    Number = decryptedSystemNumber
                },
                StartDate = new DateTime(today.Year, today.Month, today.Day),
                EndDate = new DateTime(today.Year, today.Month, today.Day)
            };

            HappyHoursCoreBL manager = new HappyHoursCoreBL();
            HappyHourSummary summaryResult = manager.GetSummary(loginParameters);

            var dayHour = summaryResult.DayDetails.FirstOrDefault();

            if (dayHour == null)
                return;

            HandleUserTime(user, dayHour, summaryResult.User, db);
        }

        private void HandleUserTime(User user, DayHours dayHour, string userName, dbDataContext db)
        {
            if (dayHour.StartTime == null && dayHour.EndTime == null)
                return;

            var result = db.sp_CheckAndUpdateUserTimes(user.Id, dayHour.StartTime, dayHour.EndTime).FirstOrDefault();

            if (result.NewArrivalTime == true)
            {
                SendArrivalEmail(user.Email, userName, dayHour.StartTime.Value);
            }

            if (result.NewExitTime == true)
            {
                SendExitEmail(user.Email, userName, dayHour.EndTime.Value);
            }
        }

        private void SendArrivalEmail(string email, string user, DateTime arrivalTime)
        {
            GMailEmailSender.SendEmail(new GmailSendEmailParameters()
            {
                From = ConfigHelper.Config.SMTP.From,
                FromDisplayName = ConfigHelper.Config.SMTP.FromDisplayName,
                Subject = "Arrival time updated",
                To = email,
                Body = EmailTemplateManager.CreateArrivalEmailBody(new ArrivalEmailParameters()
                {
                    User = user,
                    Time = ParseTime(arrivalTime)
                })
            });
        }

        private void SendExitEmail(string email, string user, DateTime exitTime)
        {
            GMailEmailSender.SendEmail(new GmailSendEmailParameters()
            {
                From = ConfigHelper.Config.SMTP.From,
                FromDisplayName = ConfigHelper.Config.SMTP.FromDisplayName,
                Subject = "Exit time updated",
                To = email,
                Body = EmailTemplateManager.CreateExitEmailBody(new ExitEmailParameters()
                {
                    User = user,
                    Time = ParseTime(exitTime)
                })
            });
        }

        private string ParseTime(DateTime time)
        {
            return time.ToString("dd'/'MM'/'yyyy' 'HH':'mm");
        }

        private IEnumerable<User> GetAllUsers(dbDataContext db)
        {
            var users = db.sp_GetUserDetails().ToList().Select(c => new User()
            {
                Id = c.Id,
                CreatedDate = c.CreatedDate,
                Email = c.Email,
                IsEmailVerified = c.IsEmailVerified,
                Password = c.Password,
                SystemEmail = c.SystemEmail,
                SystemNumber = c.SystemNumber,
                SystemPassword = c.SystemPassword
            }).ToList();

            return users;
        }
    }
}
