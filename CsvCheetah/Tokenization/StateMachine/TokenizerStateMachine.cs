using System.Text;

namespace tobixdev.github.io.CsvCheetah.Tokenization.StateMachine
{
    public class TokenizerStateMachine : ITokenizerStateMachine, ITokenizerStateContext
    {
        private const int InitialBuilderCapacity = 500;

        private readonly StringBuilder _tokenBuilder;

        public TokenizerStateMachine(StateHolder stateHolder)
        {
            State = stateHolder.Default;
            _tokenBuilder = new StringBuilder(InitialBuilderCapacity);
        }

        public ITokenizerState State { get; set; }
        public bool WasLastTokenInRecord { get; set; }

        public Token? AcceptNextCharacter(char nextCharacter)
        {
            return State.AcceptNextCharacter(this, nextCharacter);
        }

        public Token? Finish()
        {
            return State.Finish(this);
        }

        public bool ShouldEmitNewRecordToken()
        {
            var result = WasLastTokenInRecord;
            WasLastTokenInRecord = false;
            return result;
        }

        public string ResetToken()
        {
            var result = _tokenBuilder.ToString();
            _tokenBuilder.Clear();
            return result;
        }

        public void AppendCharacter(char character)
        {
            _tokenBuilder.Append(character);
        }
    }
}