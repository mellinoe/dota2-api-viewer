using System;

namespace DotaApiViewer
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

        public static string ToQueryFormat(this Language l)
        {
            switch (l)
            {
                case Language.English:
                    return "en";
                default:
                    throw new InvalidOperationException("Illegal Language value: " + l);
            }
        }
    }
}
