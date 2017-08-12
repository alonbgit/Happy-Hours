using HappyHours.Dal.Dataset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.Helpers
{
    public static class Storage
    {
        private static bool _initialized = false;

        public static void Initialize()
        {
            if (_initialized)
                throw new Exception("Storage was already initialized");

            _initialized = true;
        }
    }
}
