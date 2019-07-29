using System.Runtime.Serialization;

namespace tobixdev.github.io.FastCsv.Tokenization.StateMachine.States
{
    public class EscapedStateWithOneQuote : StateBase
    {
        public override Token? AcceptNextCharacter(ITokenizerStateContext stateContext, char character)
        {
            switch (character)
            {
                case '"':
                    return Quote(stateContext);
                case ',':
                    return TokenFinished(stateContext);
                case '\n': // TODO: RFC conformity would require \r\n
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
            return Token.CreateValueToken(stateContext.ResetToken());
        }

        private Token? Quote(ITokenizerStateContext stateContext)
        {
            stateContext.State = StateHolder.Escaped;
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