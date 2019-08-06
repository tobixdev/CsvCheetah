using System.Collections.Generic;
using System.IO;

namespace tobixdev.github.io.CsvCheetah.Tokenization
{
    public interface ITokenizer
    {
        IEnumerable<Token> Tokenize(TextReader textReader);
    }
}