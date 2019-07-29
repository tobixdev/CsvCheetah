using System.Collections.Generic;
using System.IO;
using tobixdev.github.io.FastCsv.Tokenization.StateMachine;
using tobixdev.github.io.FastCsv.Util;

namespace tobixdev.github.io.FastCsv.Tokenization
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
        }

        private IEnumerable<Token> TokenizeBuffer(IReadOnlyList<char> buffer, int readChars)
        {
            // TODO: Check performance penalty for LINQ, after first benchmark
            for (var index = 0; index < readChars; index++)
            {
                var character = buffer[index];
                var result = _tokenizerStateMachine.AcceptNextCharacter(character);
                if (result.HasValue)
                    yield return result.Value;
            }
        }
    }
}