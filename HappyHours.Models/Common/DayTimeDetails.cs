using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Models.Common
{
    public class DayTimeDetails
    {
        public string Date { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string Day { get; set; }

        public int ExtraMinutes { get; set; }

        public int LackMinutes { get; set; }
    }
}
