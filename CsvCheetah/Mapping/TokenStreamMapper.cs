using System;
using System.Collections.Generic;
using System.Linq;

namespace tobixdev.github.io.CsvCheetah.Mapping
{
    public class TokenStreamMapper<T> : ITokenStreamMapper<T>
    {
        private readonly Action<T, object>[] _setters;

        public TokenStreamMapper(Action<T, object>[] setters)
        {
            _setters = setters;
        }

        public IEnumerable<T> Map(IEnumerable<Token> tokenStream)
        {
            return Enumerable.Empty<T>();
        }
    }
}