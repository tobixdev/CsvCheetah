using System.Threading;
using tobixdev.github.io.FastCsv.Tokenization.StateMachine.States;

namespace tobixdev.github.io.FastCsv.Tokenization.StateMachine
{
    public static class StateHolder
    {
        public static readonly ITokenizerState Default = new DefaultState();
        public static readonly ITokenizerState Escaped = new EscapedState();
        public static readonly ITokenizerState EscapedWithOneQuote = new EscapedStateWithOneQuote();
    }
}