namespace tobixdev.github.io.CsvCheetah.Mapping.Conversion
{
    public abstract class SimpleConverterBase<T> : IConverter
    {
        protected abstract bool TryParse(string value, out T parsedValue);
        
        public object Convert(string value)
        {
            var canConvert = TryParse(value, out var parsedValue);
            
            if(!canConvert)
                throw new MappingException($"Cannot convert '{value}' to {typeof(T).Name}.");

            return parsedValue;
        }
    }
}