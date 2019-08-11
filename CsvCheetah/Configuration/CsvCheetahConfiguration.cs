namespace tobixdev.github.io.CsvCheetah.Configuration
{
    public class CsvCheetahConfiguration : ICsvCheetahConfiguration
    {
        public CsvCheetahConfiguration()
        {
            ColumnDelimiter = ',';
        }

        public char ColumnDelimiter { get; set; }
    }
}