namespace tobixdev.github.io.FastCsv.Tokenization.StateMachine.States
{
    public abstract class StateBase : ITokenizerState
    {
        // TODO: Remove if unecessary
        public abstract Token? AcceptNextCharacter(ITokenizerStateContext stateContext, char character);
        public abstract Token? Finish(ITokenizerStateContext tokenizerStateMachine);
    }
}