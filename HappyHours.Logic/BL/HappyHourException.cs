using HappyHours.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.BL
{
    public class HappyHourException : Exception
    {
        public HappyHourException(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        public ErrorCode ErrorCode
        {
            get;
            private set;
        }
    }
}
