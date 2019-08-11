using System;

namespace tobixdev.github.io.CsvCheetah.Util
{
    internal static class ArgumentUtility
    {
        internal static void IsNotNull(string name, object value)
        {
            if (value == null)
                throw new ArgumentNullException(name);
        }
    }
}