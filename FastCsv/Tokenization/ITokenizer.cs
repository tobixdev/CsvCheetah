using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace tobixdev.github.io.FastCsv.Tokenization
{
    public interface ITokenizer
    {
        IEnumerable<Token> Tokenize(TextReader textReader);
    }
}