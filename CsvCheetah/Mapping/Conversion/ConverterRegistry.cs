using System.Collections;

namespace tobixdev.github.io.CsvCheetah.Mapping.Conversion
{
    public class ConverterRegistry : IConverterRegistry
    {
        private readonly Hashtable _converters;

        public ConverterRegistry()
        {
            _converters = new Hashtable();
        }

        public void RegisterConverter<T>(IConverter<T> converter)
        {
            var fullType = GetFullType<T>();
            _converters[fullType] = converter;
        }

        public IConverter<T> GetConverter<T>()
        {
            var fullType = GetFullType<T>();
            var converter = (IConverter<T>) _converters[fullType];
            
            if(converter == null)
                throw new MappingException($"No converter for type {typeof(T).Name}.");

            return converter;
        }

        private string GetFullType<T>()
        {
            return typeof(T).FullName;
        }
    }
}