namespace tobixdev.github.io.CsvCheetah.Tokenization.StateMachine
{
    public interface ITokenizerState
    {
        Token? AcceptNextCharacter(ITokenizerStateContext stateContext, char character);
        Token? Finish(ITokenizerStateContext stateContext);
    }
}