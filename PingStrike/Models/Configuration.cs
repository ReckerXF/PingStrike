using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PingStrike.Server.Models
{
    internal class Configuration
    {
        public struct Config
        {
            public int pingThreshold { get; set; }
            public int pollingRate { get; set; }
            public int refreshRate { get; set; }
            public int maxStrikes { get; set; }
        }
    }
}
