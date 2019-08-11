using tobixdev.github.io.CsvCheetah.Configuration;

namespace tobixdev.github.io.CsvCheetah.Tokenization.StateMachine.States
{
    public class EscapedStateWithOneQuote : StateBase
    {
        private readonly StateHolder _stateHolder;
        private readonly ICsvCheetahConfiguration _configuration;

        public EscapedStateWithOneQuote(StateHolder stateHolder, ICsvCheetahConfiguration configuration)
        {
            _stateHolder = stateHolder;
            _configuration = configuration;
        }

        public override Token? AcceptNextCharacter(ITokenizerStateContext stateContext, char character)
        {
            if (character == '"')
                return Quote(stateContext);
            
            if (character == _configuration.ColumnDelimiter)
                return TokenFinished(stateContext);
            
            if (character == '\n')
                return RecordFinished(stateContext);
            
            return NormalCharacter();
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