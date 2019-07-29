using System.Text;

namespace tobixdev.github.io.FastCsv.Tokenization.StateMachine
{
    public class TokenizerStateMachine : ITokenizerStateMachine, ITokenizerStateContext
    {
        private const int c_initialBuilderCapacity = 500;

        private readonly StringBuilder _tokenBuilder;

        public TokenizerStateMachine()
        {
            State = StateHolder.Start;
            _tokenBuilder = new StringBuilder(c_initialBuilderCapacity);
        }

        public Token? AcceptNextCharacter(char nextCharacter)
        {
            return State.AcceptNextCharacter(this, nextCharacter);
        }

        public ITokenizerState State { private get; set; }

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