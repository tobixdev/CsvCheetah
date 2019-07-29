namespace tobixdev.github.io.FastCsv.Tokenization.RFC4180.StateMachine
{
    public interface ITokenizerStateMachine
    {
        Token? AcceptNextChar(char nextCharacter);
    }
}