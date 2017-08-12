
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.Core
{
    public class HappyHourSummary
    {
        public int ExtraMinutes { get; set; }

        public int LackMinutes { get; set; }

        public IEnumerable<DayHours> DayDetails { get; set; }

        public string User { get; set; }
    }
}
