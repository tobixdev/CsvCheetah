namespace tobixdev.github.io.CsvCheetah.Tokenization.StateMachine.States
{
    public abstract class StateBase : ITokenizerState
    {
        public abstract Token? AcceptNextCharacter(ITokenizerStateContext stateContext, char character);
        public abstract Token? Finish(ITokenizerStateContext stateContext);
    }
}