using System;

namespace AutomationBase.Utility
{
    public sealed class StringUtilities
    {
        /// <summary>
        /// Determines if the given string is mostly numberical characters, must be greater than 50% of the string to return true
        /// </summary>
        /// <param name="str"></param>
        /// <returns>true if numOfDigits > 50% of str.Length, false otherwise</returns>
        public static bool StrIsMostlyDigits(string str)
        {
            double digitCnt = 0;
            foreach (char c in str)
                if (Char.IsDigit(c))
                    digitCnt++;
            return (digitCnt / ((double)str.Length)) > 0.5;
        }
        
        /// <summary>
        /// Takes (almost) any currency as a string, with or without '$' signs and '.', and returens a double value it represents
        /// </summary>
        /// <param name="currancyStr"></param>
        /// <returns></returns>
        public static double TextCurrencyToDouble(string currancyStr)
        {
            return Double.Parse(currancyStr.Replace("$", "").Replace(" ", "").Trim());
        }

        public static String GetCurrentTimeStamp()
        {
            String currentStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return currentStamp;
        }
    }
}
