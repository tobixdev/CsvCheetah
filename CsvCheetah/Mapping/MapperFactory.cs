namespace tobixdev.github.io.CsvCheetah.Mapping
{
    public class MapperFactory<T> : IMapperFactory<T>
    {
        public ITokenStreamMapper<T> CreateForMap(IMap<T> map)
        {
            return new TokenStreamMapper<T>();
        }
    }
}