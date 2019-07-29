namespace tobixdev.github.io.FastCsv.Tokenization.StateMachine
{
    public interface ITokenizerState
    {
        Token? AcceptNextCharacter(ITokenizerStateContext stateContext, char character);
    }
}