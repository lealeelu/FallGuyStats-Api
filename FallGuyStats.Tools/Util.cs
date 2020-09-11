// FallGuyStats Copyright (c) Leah Lee. All rights reserved.
// Licensed under the GNU General Public License v3.0
// Please Consider supporting the developer with some good good coffee: ko-fi.com/lealeelu

using System;

namespace FallGuyStats.Tools
{
    public static class Util
    {
        public static int IntParse(string s, int defaultValue = 0)
        {
            int result;
            if (int.TryParse(s, out result))
            {
                return result;
            }
            else return defaultValue;
        }

        public static bool BoolParse(string s, bool defaultBool = false)
        {
            if (string.Equals(s, "True")) return true;
            else return defaultBool;
        }

        public static DateTime DateTimeParse(string s)
        {
            //example 23:21:39.384
            DateTime dt = DateTime.Today;
            return dt.AddHours(23).AddMinutes(21).AddSeconds(39);
        }
    }
}
