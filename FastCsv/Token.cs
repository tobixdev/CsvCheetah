using System.Net.Http.Headers;

namespace tobixdev.github.io.FastCsv
{
    public struct Token
    {
        public static Token CreateValueToken(string value)
        {
            return new Token(value, TokenType.Value);
        }

        public static readonly Token DelimiterToken = new Token(null, TokenType.RecordDelimiter);

        private Token(string value, TokenType tokenType)
        {
            Value = value;
            TokenType = tokenType;
        }

        public string Value { get; }
        public TokenType TokenType { get; }
    }
}