using System;
using System.Collections.Generic;
using System.Linq;

namespace tobixdev.github.io.CsvCheetah.Mapping
{
    public class TokenStreamMapper<T> : ITokenStreamMapper<T>
    {
        private readonly Action<T, object>[] _setters;

        private int _currentProperty;
        private T _currentObject;

        public TokenStreamMapper(Action<T, object>[] setters)
        {
            _setters = setters;
        }

        public IEnumerable<T> Map(IEnumerable<Token> tokenStream)
        {
            _currentObject = Activator.CreateInstance<T>();
            
            foreach (var token in tokenStream)
            {
                switch (token.TokenType)
                {
                    case TokenType.Value:
                        _setters[_currentProperty].Invoke(_currentObject, token.Value);
                        _currentProperty++;
                        break;
                    
                    case TokenType.RecordDelimiter:
                        yield return _currentObject;
                        _currentObject = Activator.CreateInstance<T>();
                        _currentProperty = 0;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException($"Token type {token.TokenType} not supported.");
                }
            }
        }
    }
}