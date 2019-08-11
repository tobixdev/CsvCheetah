namespace tobixdev.github.io.CsvCheetah.Tokenization.StateMachine.States
{
    public class EscapedState : StateBase
    {
        public EscapedState(StateHolder stateHolder) : base(stateHolder)
        {
        }

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

        public override Token? Finish(ITokenizerStateContext stateContext)
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