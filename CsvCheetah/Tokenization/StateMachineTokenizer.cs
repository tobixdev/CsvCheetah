using System.Collections.Generic;
using System.IO;
using tobixdev.github.io.CsvCheetah.Tokenization.StateMachine;
using tobixdev.github.io.CsvCheetah.Util;

namespace tobixdev.github.io.CsvCheetah.Tokenization
{
    public class StateMachineTokenizer : ITokenizer
    {
        private const int ChunkSize = 1000;

        private readonly ITokenizerStateMachine _tokenizerStateMachine;

        public StateMachineTokenizer(ITokenizerStateMachine tokenizerStateMachine)
        {
            _tokenizerStateMachine = tokenizerStateMachine;
        }

        public IEnumerable<Token> Tokenize(TextReader textReader)
        {
            ArgumentUtility.IsNotNull(nameof(textReader), textReader);

            var buffer = new char[ChunkSize];
            int readChars;
            while ((readChars = textReader.Read(buffer, 0, ChunkSize)) > 0)
                foreach (var token in TokenizeBuffer(buffer, readChars))
                    yield return token;

            var lastToken = _tokenizerStateMachine.Finish();
            if (lastToken != null) 
                yield return lastToken.Value;
            yield return Token.DelimiterToken;
        }

        private IEnumerable<Token> TokenizeBuffer(IReadOnlyList<char> buffer, int readChars)
        {
            for (var index = 0; index < readChars; index++)
            {
                var character = buffer[index];
                var result = _tokenizerStateMachine.AcceptNextCharacter(character);

                if (result.HasValue)
                {
                    yield return result.Value;

                    if (_tokenizerStateMachine.ShouldEmitNewRecordToken())
                        yield return Token.DelimiterToken;
                }
            }
        }
    }
}