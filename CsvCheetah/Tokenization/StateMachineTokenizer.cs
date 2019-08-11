using System.Collections.Generic;
using System.IO;
using tobixdev.github.io.CsvCheetah.Tokenization.StateMachine;
using tobixdev.github.io.CsvCheetah.Util;

namespace tobixdev.github.io.CsvCheetah.Tokenization
{
    public class StateMachineTokenizer : ITokenizer
    {
        private const int ChunkSize = 1024 * 1024;

        private readonly ITokenizerStateMachine _tokenizerStateMachine;
        private readonly char[] _buffer = new char[ChunkSize];

        public StateMachineTokenizer(ITokenizerStateMachine tokenizerStateMachine)
        {
            _tokenizerStateMachine = tokenizerStateMachine;
        }

        public IEnumerable<Token> Tokenize(TextReader textReader)
        {
            ArgumentUtility.IsNotNull(nameof(textReader), textReader);

            int readChars;
            while ((readChars = textReader.Read(_buffer, 0, ChunkSize)) > 0)
                foreach (var token in TokenizeBuffer(readChars))
                    yield return token;

            var lastToken = _tokenizerStateMachine.Finish();
            if (lastToken != null) 
                yield return lastToken.Value;
            yield return Token.DelimiterToken;
        }

        private IEnumerable<Token> TokenizeBuffer(int readChars)
        {
            for (var index = 0; index < readChars; index++)
            {
                var character = _buffer[index];
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