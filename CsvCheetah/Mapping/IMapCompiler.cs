namespace tobixdev.github.io.CsvCheetah.Mapping
{
    public interface IMapCompiler<T>
    {
        ITokenStreamMapper<T> CompileMap(IMap<T> map);
    }
}