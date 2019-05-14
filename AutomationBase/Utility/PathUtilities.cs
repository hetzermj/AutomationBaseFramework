using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationBase.Utility
{
    class PathUtilities
    {
        public static String LocalDirPath;
        public static String ReportPath;

        public static String getLocalDirPath()
        {
            LocalDirPath = System.IO.Directory.GetCurrentDirectory();    
            return LocalDirPath;
        }




    }
}
