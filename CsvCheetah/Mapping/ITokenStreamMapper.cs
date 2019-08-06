using System.Collections.Generic;

namespace tobixdev.github.io.CsvCheetah.Mapping
{
    public interface ITokenStreamMapper<out T>
    {
        IEnumerable<T> Map(IEnumerable<Token> tokenStream);
    }
}