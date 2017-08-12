using HappyHours.Logic;
using HappyHours.Logic.Core;
using HappyHours.Service.Schedulers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var _userArrivalScheduler = new UserArrivalScheduler(10 * 60 * 1000);
            _userArrivalScheduler.Start();

            Console.ReadKey();
        }
    }
}
