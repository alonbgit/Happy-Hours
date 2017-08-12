using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace HappyHours.Service.Schedulers
{
    public abstract class BaseScheduler
    {
        private Timer _timer = null;
        private int _interval;

        public BaseScheduler(int interval)
        {
            _interval = interval;
        }

        public void Start()
        {
            _timer = new Timer(_interval);
            _timer.AutoReset = true;
            _timer.Enabled = true;
            _timer.Start();

            _timer.Elapsed += _timer_Elapsed;

            Elapsed();
        }

        public void Stop()
        {
            _timer.Stop();
            _timer.Dispose();
        }

        protected abstract void Elapsed();

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.Elapsed();
        }
    }
}
