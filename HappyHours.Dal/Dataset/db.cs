using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Dal.Dataset
{
    public partial class dbDataContext
    {
        partial void OnCreated()
        {
            this.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["HappyHoursConnectionString"].ConnectionString;
        }
    }
}
