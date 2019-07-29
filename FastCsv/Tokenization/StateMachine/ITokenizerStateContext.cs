namespace tobixdev.github.io.FastCsv.Tokenization.StateMachine
{
    public interface ITokenizerStateContext
    {
        ITokenizerState State { set; }
        bool WasLastTokenInRecord { set; }
        void AppendCharacter(char character);
        string ResetToken();
    }
}