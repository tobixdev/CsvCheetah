using tobixdev.github.io.CsvCheetah.Configuration;

namespace tobixdev.github.io.CsvCheetah.Tokenization.StateMachine.States
{
    public class DefaultState : StateBase
    {
        private readonly StateHolder _stateHolder;
        private readonly ICsvCheetahConfiguration _configuration;

        public DefaultState(StateHolder stateHolder, ICsvCheetahConfiguration configuration)
        {
            _stateHolder = stateHolder;
            _configuration = configuration;
        }

        public override Token? AcceptNextCharacter(ITokenizerStateContext stateContext, char character)
        {
            if (character == _configuration.ColumnDelimiter)
                return TokenFinished(stateContext);
            
            if (character == '"')
                return Quote(stateContext);
            
            if (character == '\r')
                return StartOfRecordDelimiter(stateContext);
            
            return NormalCharacter(stateContext, character);
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
            stateContext.State = _stateHolder.Escaped;
            // TODO Throw error, if quote not at start
            return null;
        }

        private Token? NormalCharacter(ITokenizerStateContext stateContext, char character)
        {
            stateContext.AppendCharacter(character);
            return null;
        }
    }
}