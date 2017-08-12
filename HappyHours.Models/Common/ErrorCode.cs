using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Models.Common
{
    public enum ErrorCode
    {
        InvalidUser = 1,
        EmailAlreadyExist = 2,
        InvalidCredentials = 3,
        InvalidYearForEmployee = 4,
        BadRequest = 5,
        EmailNotVerified = 6,
        InvalidActivationToken = 7,
        InternalServerError = 900
    }
}
