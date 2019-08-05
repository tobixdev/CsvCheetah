namespace tobixdev.github.io.FastCsv.Tokenization.StateMachine
{
    public interface ITokenizerStateContext
    {
        ITokenizerState State { get; set; }
        bool WasLastTokenInRecord { get; set; }
        void AppendCharacter(char character);
        string ResetToken();
    }
}