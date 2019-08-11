namespace tobixdev.github.io.CsvCheetah.Mapping.Maps
{
    public interface IMutableMap<T> : IMap<T>
    {
        void AddMapping(int column, string propertyName);
    }
}