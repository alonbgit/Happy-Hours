using HappyHours.Logic.Helpers;
using HappyHours.Service.Schedulers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HappyHours.Service
{
    public partial class HappyHoursService : ServiceBase
    {
        private UserArrivalScheduler _userArrivalScheduler;

        public HappyHoursService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _userArrivalScheduler = new UserArrivalScheduler(ConfigHelper.Config.UserArrivalSchedulerInterval * 1000);
            _userArrivalScheduler.Start();
        }

        protected override void OnStop()
        {
            _userArrivalScheduler.Stop();
        }
    }
}
