using System;

namespace tobixdev.github.io.FastCsv.Util
{
    public static class ArgumentUtility
    {
        public static void IsNotNull(string name, object value)
        {
            if (value == null)
                throw new ArgumentNullException(name);
        }
    }
}