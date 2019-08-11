namespace tobixdev.github.io.CsvCheetah.Tokenization.StateMachine.States
{
    public abstract class StateBase : ITokenizerState
    {
        public abstract Token? AcceptNextCharacter(ITokenizerStateContext stateContext, char character);
        public abstract Token? Finish(ITokenizerStateContext stateContext);
        
        protected Token? StartOfRecordDelimiter(ITokenizerStateContext stateContext)
        {
            var intermediateState = new StringMatchingState(this, "\n", state =>
            {
                stateContext.WasLastTokenInRecord = true;
                return Token.CreateValueToken(stateContext.ResetToken());
            });
            stateContext.State = intermediateState;
            return null;
        }
    }
}