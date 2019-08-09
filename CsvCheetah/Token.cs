namespace tobixdev.github.io.CsvCheetah
{
    public struct Token
    {
        public static Token CreateValueToken(string value)
        {
            return new Token(value, TokenType.Value);
        }

        public static readonly Token DelimiterToken = new Token("", TokenType.RecordDelimiter);

        private Token(string value, TokenType tokenType)
        {
            Value = value;
            TokenType = tokenType;
        }

        public string Value { get; }
        public TokenType TokenType { get; }

        public override string ToString()
        {
            return $"{nameof(Value)}: {Value}, {nameof(TokenType)}: {TokenType}";
        }
    }
}