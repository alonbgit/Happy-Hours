using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.Core
{
    public class HappyHourData
    {
        public IEnumerable<HappyHourItem> Hours { get; set; }

        public string User { get; set; }
    }
}
