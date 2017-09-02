using HappyHours.Dal.Dataset;
using HappyHours.Logic.Core;
using HappyHours.Logic.Helpers;
using HappyHours.Logic.Models;
using HappyHours.Models.Common;
using HappyHours.Models.UserInformation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.BL
{
    public class UserInformationBL
    {
        public UserInformationResponse GetUserInformation(UserInformationRequest request, long userId, dbDataContext db)
        {
            var user = GetUserDetails(userId, db);

            int month = 0;

            if (request == null || request.Month == null)
                month = DateTime.Today.Month;
            else
                month = request.Month.Value;

            //var today = DateTime.Today; // new DateTime(2017, 8, 1).Date;
            var startDate = new DateTime(DateTime.Today.Year, month, 1);
            var endDate = new DateTime(DateTime.Today.Year, month, DateTime.DaysInMonth(DateTime.Today.Year, month));

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
                StartDate = startDate,
                EndDate = endDate //today // new DateTime(2017, 8, 31).Date
            };

            HappyHoursCoreBL manager = new HappyHoursCoreBL();
            HappyHourSummary summaryResult = manager.GetSummary(loginParameters);

            IList<MonthDetails> months = new List<MonthDetails>();
            CultureInfo usEnglish = new CultureInfo("en-US");
            for (var i = 1; i <= DateTime.Today.Month; i++)
            {
                months.Add(new MonthDetails()
                {
                    Month = i,
                    Name = usEnglish.DateTimeFormat.GetMonthName(i)
                });
            }

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
                }).ToList(),
                Months = months
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
