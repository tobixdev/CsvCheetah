namespace tobixdev.github.io.FastCsv.Tokenization.StateMachine.States
{
    public class DefaultState : StateBase
    {
        public override Token? AcceptNextCharacter(ITokenizerStateContext stateContext, char character)
        {
            switch (character)
            {
                case ',':
                    return TokenFinished(stateContext);
                case '"':
                    return Quote(stateContext);
                case '\r':
                    return StartOfRecordDelimiter(stateContext);
                default:
                    return NormalCharacter(stateContext, character);
            }
        }

        public override Token? Finish(ITokenizerStateContext stateContext)
        {
            return Token.CreateValueToken(stateContext.ResetToken());
        }

        private Token? TokenFinished(ITokenizerStateContext stateContext)
        {
            return Token.CreateValueToken(stateContext.ResetToken());
        }

        private Token? Quote(ITokenizerStateContext stateContext)
        {
            stateContext.State = StateHolder.Escaped;
            // TODO Throw error, if quote not at start
            return null;
        }

        private Token? StartOfRecordDelimiter(ITokenizerStateContext stateContext)
        {
            var intermediateState = new StringMatchingState(this, "\n", state =>
            {
                stateContext.WasLastTokenInRecord = true;
                return Token.CreateValueToken(stateContext.ResetToken());
            });
            stateContext.State = intermediateState;
            return null;
        }

        private Token? NormalCharacter(ITokenizerStateContext stateContext, char character)
        {
            stateContext.AppendCharacter(character);
            return null;
        }
    }
}