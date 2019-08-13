namespace tobixdev.github.io.CsvCheetah.Mapping.Conversion.PrimitiveConverter
{
    public class Int16Converter : SimpleConverterBase<short>
    {
        protected override bool TryParse(string value, out short parsedValue)
        {
            return short.TryParse(value, out parsedValue);
        }
    }
}