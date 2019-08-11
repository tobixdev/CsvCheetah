namespace tobixdev.github.io.CsvCheetah.Mapping.Maps
{
    public interface IMap<T>
    {
        bool HasDefinitionForColumn(int columnIndex);
        string GetTargetPropertyName(int columnIndex);
        int ColumnCount { get; }
    }
}