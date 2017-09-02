using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.Core
{
    public class DayHours
    {
        public DateTime Date { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string Day { get; set; }

        public int ExtraMinutes { get; set; }

        public int LackMinutes { get; set; }
    }
}
