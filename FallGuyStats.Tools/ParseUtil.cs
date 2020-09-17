// FallGuyStats Copyright (c) Leah Lee. All rights reserved.
// Licensed under the GNU General Public License v3.0
// Please Consider supporting the developer with some good good coffee: ko-fi.com/lealeelu

using System;

namespace FallGuyStats.Tools
{
    public static class ParseUtil
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
            DateTime dt;
            try
            {
                dt = DateTime.Today
                    .AddHours(IntParse(s.Substring(0, 2)))
                    .AddMinutes(IntParse(s.Substring(3, 2)))
                    .AddSeconds(IntParse(s.Substring(6, 2)));
            }
            catch
            {
                dt = DateTime.Now;
            }
            return dt;
        }
    }
}
