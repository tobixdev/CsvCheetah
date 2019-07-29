namespace tobixdev.github.io.FastCsv.Tokenization.StateMachine.States
{
    public class EscapedState : StateBase
    {
        public override Token? AcceptNextCharacter(ITokenizerStateContext stateContext, char character)
        {
            switch (character)
            {
                case '"':
                    return Quote(stateContext);
                default:
                    return NormalOrEscapedCharacter(stateContext, character);
            }
        }

        public override Token? Finish(ITokenizerStateContext tokenizerStateMachine)
        {
            throw new TokenizationException("Escaped field not closed. Unexpected end of input.");
        }

        private Token? Quote(ITokenizerStateContext stateContext)
        {
            stateContext.State = StateHolder.EscapedWithOneQuote;
            return null;
        }

        private Token? NormalOrEscapedCharacter(ITokenizerStateContext stateContext, char character)
        {
            stateContext.AppendCharacter(character);
            return null;
        }
    }
}