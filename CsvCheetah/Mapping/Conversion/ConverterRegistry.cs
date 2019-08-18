using System;
using System.Collections;
using tobixdev.github.io.CsvCheetah.Mapping.Conversion.PrimitiveConverter;

namespace tobixdev.github.io.CsvCheetah.Mapping.Conversion
{
    public class ConverterRegistry : IConverterRegistry
    {
        public static IConverterRegistry CreateDefaultInstance()
        {
            var result = new ConverterRegistry();
            result.RegisterConverter(typeof(string), new StringConverter());
            result.RegisterConverter(typeof(short),new Int16Converter());
            result.RegisterConverter(typeof(int),new Int32Converter());
            result.RegisterConverter(typeof(long),new Int64Converter());
            return result;
        }
        
        private readonly Hashtable _converters;

        public ConverterRegistry()
        {
            _converters = new Hashtable();
        }

        public void RegisterConverter(Type type, IConverter converter)
        {
            _converters[type.FullName] = converter;
        }

        public IConverter GetConverter(Type type)
        {
            var converter = _converters[type.FullName];
            
            if(converter == null)
                throw new MappingException($"No converter for type {type.Name}.");

            return (IConverter) converter;
        }
    }
}