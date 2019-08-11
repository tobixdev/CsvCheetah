using System;
using System.Collections.Generic;

namespace tobixdev.github.io.CsvCheetah.Mapping
{
    public class TokenStreamMapper<T> : ITokenStreamMapper<T>
        where T : class
    {
        private readonly Action<T, object>[] _setters;

        private int _currentProperty;
        private T? _currentObject;

        public TokenStreamMapper(Action<T, object>[] setters)
        {
            _setters = setters;
        }

        public IEnumerable<T> Map(IEnumerable<Token> tokenStream)
        {
            _currentObject = CreateInstance();

            foreach (var token in tokenStream)
            {
                switch (token.TokenType)
                {
                    case TokenType.Value:
                        if(_currentProperty >= _setters.Length)
                            continue;

                        _setters[_currentProperty].Invoke(_currentObject, token.Value);
                        _currentProperty++;
                        break;

                    case TokenType.RecordDelimiter:
                        yield return _currentObject;
                        _currentObject = CreateInstance();
                        _currentProperty = 0;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException($"Token type {token.TokenType} not supported.");
                }
            }
        }

        private T CreateInstance()
        {
            try
            {
                return Activator.CreateInstance<T>();
            }
            catch (MissingMethodException ex)
            {
                throw new MappingException($"No default constructor found for {typeof(T).Name}.", ex);
            }
            catch (Exception ex)
            {
                throw new MappingException("Unexpected exception during instance creation.", ex);
            }
        }
    }
}