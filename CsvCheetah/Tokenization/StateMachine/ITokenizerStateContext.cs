namespace tobixdev.github.io.CsvCheetah.Tokenization.StateMachine
{
    public interface ITokenizerStateContext
    {
        ITokenizerState State { get; set; }
        bool WasLastTokenInRecord { get; set; }
        void AppendCharacter(char character);
        string ResetToken();
    }
}