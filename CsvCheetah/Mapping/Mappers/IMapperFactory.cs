using tobixdev.github.io.CsvCheetah.Mapping.Maps;

namespace tobixdev.github.io.CsvCheetah.Mapping.Mappers
{
    public interface IMapperFactory<T>
    {
        ITokenStreamMapper<T> CreateForMap(IMap<T> map);
    }
}