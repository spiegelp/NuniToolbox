using System;
using System.Collections.Generic;
using System.Text;

namespace NuniToolbox.Time
{
    public sealed class YearUtil
    {
        private YearUtil() { }

        public static bool IsLeapYear(int year)
        {
            // - years divisible by 4 are leap years
            // - exception: years divisible by 100 are no leap years
            // - exception of the exception: years divisible by 400 are leap years
            if ((year % 4) == 0)
            {
                if ((year % 100) > 0)
                {
                    return true;
                }
                else
                {
                    return (year % 400) == 0;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
