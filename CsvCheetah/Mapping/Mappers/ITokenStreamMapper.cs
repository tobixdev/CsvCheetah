using System.Collections.Generic;

namespace tobixdev.github.io.CsvCheetah.Mapping.Mappers
{
    public interface ITokenStreamMapper<out T>
    {
        IEnumerable<T> Map(IEnumerable<Token> tokenStream);
    }
}