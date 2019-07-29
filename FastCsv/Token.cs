namespace tobixdev.github.io.FastCsv
{
    public struct Token
    {
        public Token(string value, TokenType tokenType)
        {
            Value = value;
            TokenType = tokenType;
        }

        public string Value { get; }
        public TokenType TokenType { get; }
    }
}