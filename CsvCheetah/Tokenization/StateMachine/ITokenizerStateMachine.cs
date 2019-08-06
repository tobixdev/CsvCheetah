namespace tobixdev.github.io.CsvCheetah.Tokenization.StateMachine
{
    public interface ITokenizerStateMachine
    {
        Token? AcceptNextCharacter(char nextCharacter);
        Token? Finish();
        bool ShouldEmitNewRecordToken();
    }
}