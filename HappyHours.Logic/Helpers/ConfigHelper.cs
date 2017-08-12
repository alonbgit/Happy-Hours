using HappyHours.Logic.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyHours.Logic.Helpers
{
    public static class ConfigHelper
    {
        public static readonly string ConnectionString = ConfigurationManager.ConnectionStrings["HappyHoursConnectionString"].ConnectionString;

        public static readonly HappyHoursConfiguration Config = ReadConfig();

        private static HappyHoursConfiguration ReadConfig()
        {
            var configPath = ConfigurationManager.AppSettings["ConfigPath"];
            var config = JsonConvert.DeserializeObject<HappyHoursConfiguration>(File.ReadAllText(configPath));
            return config;
        }
    }
}
