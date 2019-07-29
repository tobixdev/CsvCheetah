using System.Threading;
using tobixdev.github.io.FastCsv.Tokenization.StateMachine.States;

namespace tobixdev.github.io.FastCsv.Tokenization.StateMachine
{
    public static class StateHolder
    {
        public static ITokenizerState Start = new StartState();
    }
}