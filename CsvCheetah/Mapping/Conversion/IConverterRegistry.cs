namespace tobixdev.github.io.CsvCheetah.Mapping.Conversion
{
    public interface IConverterRegistry
    {
        void RegisterConverter<T>(IConverter<T> converter);
        IConverter<T> GetConverter<T>();
    }
}