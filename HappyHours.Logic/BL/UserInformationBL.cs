using HappyHours.Dal.Dataset;
using HappyHours.Logic.Core;
using HappyHours.Logic.Helpers;
using HappyHours.Logic.Models;
using HappyHours.Models.Common;
using HappyHours.Models.UserInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.BL
{
    public class UserInformationBL
    {
        public UserInformationResponse GetUserInformation(BaseRequest request, long userId, dbDataContext db)
        {
            var user = GetUserDetails(userId, db);

            var today = new DateTime(2017, 8, 1).Date; //DateTime.Today;

            var decryptedSystemPassword = PasswordEncryptor.Decrypt(user.SystemPassword);
            var decryptedSystemNumber = PasswordEncryptor.Decrypt(user.SystemNumber);

            var loginParameters = new HappyHoursLoginParameters()
            {
                Credentials = new HappyHoursCredentials()
                {
                    Username = user.SystemEmail,
                    Password = decryptedSystemPassword,
                    Number = decryptedSystemNumber
                },
                StartDate = new DateTime(today.Year, today.Month, 1),
                EndDate = new DateTime(2017, 8, 31).Date //today
            };

            HappyHoursCoreBL manager = new HappyHoursCoreBL();
            HappyHourSummary summaryResult = manager.GetSummary(loginParameters);

            return new UserInformationResponse()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                ExtraMinutes = summaryResult.ExtraMinutes,
                LackMinutes = summaryResult.LackMinutes,
                Days = summaryResult.DayDetails.Select(c => new DayTimeDetails()
                {
                    ExtraMinutes = c.ExtraMinutes,
                    LackMinutes = c.LackMinutes,
                    Date = HappyHourTimestampProvider.GetDateTimestamp(c.Date),
                    StartTime = HappyHourTimestampProvider.GetDateTimeTimestamp(c.StartTime),
                    EndTime = HappyHourTimestampProvider.GetDateTimeTimestamp(c.EndTime),
                    Day = c.Day
                }).ToList()
            };
        }

        private User GetUserDetails(long userId, dbDataContext db)
        {
            var user = db.sp_GetUserById(userId).Select(c => new User()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                CreatedDate = c.CreatedDate,
                Email = c.Email,
                IsEmailVerified = c.IsEmailVerified,
                Password = c.Password,
                SystemEmail = c.SystemEmail,
                SystemNumber = c.SystemNumber,
                SystemPassword = c.SystemPassword
            }).FirstOrDefault();

            return user;
        }
    }
}
