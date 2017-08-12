using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.Core
{
    public class HappyHoursLoginParameters
    {
        public HappyHoursCredentials Credentials { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime StartDate { get; set; }
    }
}
