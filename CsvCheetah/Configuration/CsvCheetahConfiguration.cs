namespace tobixdev.github.io.CsvCheetah.Configuration
{
    public class CsvCheetahConfiguration : ICsvCheetahConfiguration
    {
        public CsvCheetahConfiguration()
        {
            FieldDelimiter = ',';
        }

        public char FieldDelimiter { get; set; }
    }
}