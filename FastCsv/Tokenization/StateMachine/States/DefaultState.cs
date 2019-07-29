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
                case '\n': // TODO: RFC conformity would require \r\n
                    return RecordFinished(stateContext);
                default:
                    return NormalCharacter(stateContext, character);
            }
        }

        public override Token? Finish(ITokenizerStateContext tokenizerStateMachine)
        {
            return Token.CreateValueToken(tokenizerStateMachine.ResetToken());
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

        private Token? RecordFinished(ITokenizerStateContext stateContext)
        {
            stateContext.WasLastTokenInRecord = true;
            return Token.CreateValueToken(stateContext.ResetToken());
        }

        private Token? NormalCharacter(ITokenizerStateContext stateContext, char character)
        {
            stateContext.AppendCharacter(character);
            return null;
        }

    }
}