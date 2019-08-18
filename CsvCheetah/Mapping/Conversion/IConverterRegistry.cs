using System;

namespace tobixdev.github.io.CsvCheetah.Mapping.Conversion
{
    public interface IConverterRegistry
    {
        void RegisterConverter(Type type, IConverter converter);
        IConverter GetConverter(Type type);
    }
}