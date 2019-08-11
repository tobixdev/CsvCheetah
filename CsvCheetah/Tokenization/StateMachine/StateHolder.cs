using tobixdev.github.io.CsvCheetah.Configuration;
using tobixdev.github.io.CsvCheetah.Tokenization.StateMachine.States;

namespace tobixdev.github.io.CsvCheetah.Tokenization.StateMachine
{
    public class StateHolder
    {
        public static readonly StateHolder DefaultConfiguration = new StateHolder(new CsvCheetahConfiguration());
        
        public readonly ITokenizerState Default;
        public readonly ITokenizerState Escaped;
        public readonly ITokenizerState EscapedWithOneQuote;

        public StateHolder(ICsvCheetahConfiguration configuration)
        {
            Default = new DefaultState(this, configuration);
            Escaped = new EscapedState(this);
            EscapedWithOneQuote = new EscapedStateWithOneQuote(this, configuration);
        }
    }
}