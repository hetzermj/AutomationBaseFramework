using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationBase.Utility
{
	public class SQL_Utilities
	{
		public const string CONNECTION_STRING = @"Server=ServerNameHere; Database=DatabaseNameHere; Trusted_Connection=Yes";

        public static String GetStringOrNull(Object obj)
        {
            if (obj == DBNull.Value)
            {
                return null;
            }

            return obj.ToString();
        }
	}
}
