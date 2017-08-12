using HappyHours.Dal.Dataset;
using HappyHours.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.BL
{
    public class ActivateEmailBL
    {
        public void ActivateEmail(string token, dbDataContext db)
        {
            Guid guid;
            if (!Guid.TryParse(token, out guid))
                throw new HappyHourException(ErrorCode.InvalidActivationToken);

            var result = db.sp_ActivateEmail(guid).FirstOrDefault();

            if (result.InvalidToken == true)
                throw new HappyHourException(ErrorCode.InvalidActivationToken);
        }
    }
}
