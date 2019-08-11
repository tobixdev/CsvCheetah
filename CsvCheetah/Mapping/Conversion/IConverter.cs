namespace tobixdev.github.io.CsvCheetah.Mapping.Conversion
{
    public interface IConverter<out T>
    {
        T Convert(string value);
    }
}