using tobixdev.github.io.CsvCheetah.Configuration;

namespace tobixdev.github.io.CsvCheetah.Tokenization.StateMachine.States
{
    public class EscapedStateWithOneQuote : StateBase
    {
        private readonly ICsvCheetahConfiguration _configuration;

        public EscapedStateWithOneQuote(StateHolder stateHolder, ICsvCheetahConfiguration configuration) : base(
            stateHolder)
        {
            _configuration = configuration;
        }

        public override Token? AcceptNextCharacter(ITokenizerStateContext stateContext, char character)
        {
            if (character == '"')
                return Quote(stateContext);

            if (character == _configuration.FieldDelimiter)
                return TokenFinished(stateContext);

            if (character == '\r')
                return StartOfRecordDelimiter(stateContext);

            return NormalCharacter();
        }

        public override Token? Finish(ITokenizerStateContext stateContext)
        {
            return Token.CreateValueToken(stateContext.ResetToken());
        }

        private Token? TokenFinished(ITokenizerStateContext stateContext)
        {
            stateContext.State = StateHolder.Default;
            return Token.CreateValueToken(stateContext.ResetToken());
        }

        private Token? Quote(ITokenizerStateContext stateContext)
        {
            stateContext.State = StateHolder.Escaped;
            stateContext.AppendCharacter('"');
            return null;
        }

        private Token? NormalCharacter()
        {
            throw new TokenizationException("Text followed closing qoute, in an escaped field.");
        }
    }
}