namespace tobixdev.github.io.FastCsv.Tokenization.StateMachine
{
    public interface ITokenizerStateMachine
    {
        Token? AcceptNextCharacter(char nextCharacter);
        Token? Finish();
        bool ShouldEmitNewRecordToken();
    }
}