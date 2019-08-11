namespace tobixdev.github.io.CsvCheetah.Tokenization.StateMachine.States
{
    public class EscapedStateWithOneQuote : StateBase
    {
        private readonly StateHolder _stateHolder;

        public EscapedStateWithOneQuote(StateHolder stateHolder)
        {
            _stateHolder = stateHolder;
        }

        public override Token? AcceptNextCharacter(ITokenizerStateContext stateContext, char character)
        {
            switch (character)
            {
                case '"':
                    return Quote(stateContext);
                case ',':
                    return TokenFinished(stateContext);
                case '\n':
                    return RecordFinished(stateContext);
                default:
                    return NormalCharacter();
            }
        }

        public override Token? Finish(ITokenizerStateContext stateContext)
        {
            return Token.CreateValueToken(stateContext.ResetToken());
        }

        private Token? TokenFinished(ITokenizerStateContext stateContext)
        {
            stateContext.State = _stateHolder.Default;
            return Token.CreateValueToken(stateContext.ResetToken());
        }

        private Token? Quote(ITokenizerStateContext stateContext)
        {
            stateContext.State = _stateHolder.Escaped;
            stateContext.AppendCharacter('"');
            return null;
        }

        private Token? RecordFinished(ITokenizerStateContext stateContext)
        {
            stateContext.WasLastTokenInRecord = true;
            return Token.CreateValueToken(stateContext.ResetToken());
        }

        private Token? NormalCharacter()
        {
            throw new TokenizationException("Text followed closing qoute, in an escaped field.");
        }
    }
}