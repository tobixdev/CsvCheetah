using System;
using System.Collections.Generic;
using System.Linq;

namespace tobixdev.github.io.CsvCheetah.Mapping
{
    class TokenStreamMapper<T> : ITokenStreamMapper<T>
    {
        
        public IEnumerable<T> Map(IEnumerable<Token> tokenStream)
        {
            return Enumerable.Empty<T>();
        }
    }
}