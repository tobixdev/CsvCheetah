using System;
using System.Linq.Expressions;
using tobixdev.github.io.CsvCheetah.Mapping.Conversion;
using tobixdev.github.io.CsvCheetah.Mapping.Maps;

namespace tobixdev.github.io.CsvCheetah.Mapping.Mappers
{
    public class MapperFactory<T> : IMapperFactory<T> where T : class
    {
        private readonly Action<T, object> _emptyAction = (a, b) => { };

        private readonly IConverterRegistry _converterRegistry;
        
        public MapperFactory(IConverterRegistry converterRegistry)
        {
            _converterRegistry = converterRegistry;
        }

        public ITokenStreamMapper<T> CreateForMap(IMap<T> map)
        {
            var setters = CreateSetterDelegates();
            var converters = GetConverters();
            
            return new TokenStreamMapper<T>(setters, converters);

            Action<T, object>[] CreateSetterDelegates()
            {
                var result = new Action<T, object>[map.ColumnCount];
                for (var i = 0; i < map.ColumnCount; i++)
                    result[i] = map.HasDefinitionForColumn(i) ? GetPropSetter(map.GetTargetPropertyName(i)) : _emptyAction;
                return result;
            }

            IConverter[] GetConverters()
            {
                var result = new IConverter[map.ColumnCount];

                for (var i = 0; i < map.ColumnCount; i++)
                {
                    var targetType = map.GetTargetPropertyType(i);
                    result[i] = _converterRegistry.GetConverter(targetType);
                }

                return result;
            }
        }

        private static Action<T, object> GetPropSetter(string propertyName)
        {
            var propertyType = GetPropertyType();

            var targetType = Expression.Parameter(typeof(T));
            var valueExpression = Expression.Parameter(typeof(object), "value");
            var assignmentExpression = CreateAssignmentExpression();

            return Expression.Lambda<Action<T, object>>
            (
                assignmentExpression, targetType, valueExpression
            ).Compile();

            Type GetPropertyType()
            {
                var property = typeof(T).GetProperty(propertyName);

                if (property == null)
                    throw new MappingException($"Property {propertyName} does not exists on type {typeof(T)}.");

                return property.PropertyType;
            }

            BinaryExpression CreateAssignmentExpression()
            {
                var convertedValueExpression = Expression.Convert(valueExpression, propertyType);
                var propertyExpression = Expression.Property(targetType, propertyName);

                return Expression.Assign(propertyExpression, convertedValueExpression);
            }
        }
    }
}