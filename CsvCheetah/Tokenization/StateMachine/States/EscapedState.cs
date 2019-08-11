namespace tobixdev.github.io.CsvCheetah.Tokenization.StateMachine.States
{
    public class EscapedState : StateBase
    {
        private readonly StateHolder _stateHolder;

        public EscapedState(StateHolder stateHolder)
        {
            _stateHolder = stateHolder;
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
            stateContext.State = _stateHolder.EscapedWithOneQuote;
            return null;
        }

        private Token? NormalOrEscapedCharacter(ITokenizerStateContext stateContext, char character)
        {
            stateContext.AppendCharacter(character);
            return null;
        }
    }
}