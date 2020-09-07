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
            DateTime dt;
            if (DateTime.TryParse(s, out dt)) return dt;
            else return DateTime.Now;
        }
    }
}
