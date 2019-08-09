namespace tobixdev.github.io.CsvCheetah.Mapping
{
    public interface IMapperFactory<T>
    {
        ITokenStreamMapper<T> CreateForMap(IMap<T> map);
    }
}