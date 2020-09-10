using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkRouteMonitor.Helper
{
    public static class ConfigVariables
    {
        public static int BlockHeight = int.Parse(ConfigurationManager.AppSettings["BlockHeight"]);
        public static int BlockWidth = int.Parse(ConfigurationManager.AppSettings["BlockWidth"]);
        public static int UpperMargin = int.Parse(ConfigurationManager.AppSettings["UpperMargin"]);
        public static int LeftMargin = int.Parse(ConfigurationManager.AppSettings["LeftMargin"]);
    }
}
