using System;
using System.Collections.Generic;
using System.Linq;

namespace tobixdev.github.io.CsvCheetah.Tokenization.StateMachine.States
{
    public class StringMatchingState : ITokenizerState
    {
        private readonly ITokenizerState _originalState;
        private readonly char[] _expectedCharacters;
        private readonly Func<ITokenizerStateContext, Token?> _onMatch;
        private int _nextCharacter;

        public StringMatchingState(ITokenizerState originalState, string toMatch, Func<ITokenizerStateContext, Token?> onMatch)
        {
            _originalState = originalState;
            _expectedCharacters = toMatch.ToArray();
            _onMatch = onMatch;
            _nextCharacter = 0;
        }

        private IEnumerable<char> ConsumedCharacters => _expectedCharacters.Take(_nextCharacter);

        public Token? AcceptNextCharacter(ITokenizerStateContext stateContext, char character)
        {
            if (CharacterDoesNotFit()) 
                return Fallback(stateContext, ConsumedCharacters.Append(character));
            
            _nextCharacter++;

            if (RemainingCharacters()) 
                return null;
            
            stateContext.State = _originalState;
            return _onMatch(stateContext);

            bool CharacterDoesNotFit()
            {
                return _expectedCharacters[_nextCharacter] != character;
            }

            bool RemainingCharacters()
            {
                return _expectedCharacters.Length > _nextCharacter;
            }
        }

        public Token? Finish(ITokenizerStateContext stateContext)
        {
            return Fallback(stateContext, ConsumedCharacters);
        }

        private Token? Fallback(ITokenizerStateContext stateContext, IEnumerable<char> consumedCharacters)
        {
            Token? token = null;
            foreach (var character in consumedCharacters)
            {
                var returnedToken = _originalState.AcceptNextCharacter(stateContext, character);
                if (CanHoldToken(returnedToken))
                    token = returnedToken;
                else if (returnedToken.HasValue)
                    throw new TokenizationException("FallbackState returned more than 1 tokens."); 
            }

            stateContext.State = _originalState;
            return token;
            
            bool CanHoldToken(Token? returnedToken)
            {
                return returnedToken.HasValue && !token.HasValue;
            }
        }
    }
}