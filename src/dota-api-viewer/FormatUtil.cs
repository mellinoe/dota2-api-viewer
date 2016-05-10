﻿namespace DotaApiViewer
{
    public static class FormatUtil
    {
        public static string ToString(this bool b, string trueString, string falseString)
        {
            return b ? trueString : falseString;
        }

        public static string ToCheckbox(this bool b)
        {
            return ToString(b, "[X]", "[ ]");
        }
    }
}
