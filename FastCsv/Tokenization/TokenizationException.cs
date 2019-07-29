using System;

namespace tobixdev.github.io.FastCsv.Tokenization
{
    public class TokenizationException : Exception
    {
        public TokenizationException(string message) : base(message)
        {
        }

        public TokenizationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}