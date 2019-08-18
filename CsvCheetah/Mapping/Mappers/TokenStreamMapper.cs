using System;
using System.Collections.Generic;
using tobixdev.github.io.CsvCheetah.Mapping.Conversion;

namespace tobixdev.github.io.CsvCheetah.Mapping.Mappers
{
    public class TokenStreamMapper<T> : ITokenStreamMapper<T>
        where T : class
    {
        private readonly Action<T, object>[] _setters;
        private readonly IConverter[] _converters;

        private int _currentProperty;
        private T? _currentObject;

        public TokenStreamMapper(Action<T, object>[] setters, IConverter[] converters)
        {
            _setters = setters;
            _converters = converters;
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

                        var convertedValue = _converters[_currentProperty].Convert(token.Value);
                        _setters[_currentProperty].Invoke(_currentObject, convertedValue);
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