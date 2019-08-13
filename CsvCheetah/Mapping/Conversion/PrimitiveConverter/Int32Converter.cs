namespace tobixdev.github.io.CsvCheetah.Mapping.Conversion.PrimitiveConverter
{
    public class Int32Converter : SimpleConverterBase<int>
    {
        protected override bool TryParse(string value, out int parsedValue)
        {
            return int.TryParse(value, out parsedValue);
        }
    }
}