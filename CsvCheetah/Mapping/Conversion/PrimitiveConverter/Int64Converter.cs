namespace tobixdev.github.io.CsvCheetah.Mapping.Conversion.PrimitiveConverter
{
    public class Int64Converter : SimpleConverterBase<long>
    {
        protected override bool TryParse(string value, out long parsedValue)
        {
            return long.TryParse(value, out parsedValue);
        }
    }
}