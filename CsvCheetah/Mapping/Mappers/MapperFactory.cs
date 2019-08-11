using System;
using System.Linq.Expressions;
using tobixdev.github.io.CsvCheetah.Mapping.Maps;

namespace tobixdev.github.io.CsvCheetah.Mapping.Mappers
{
    public class MapperFactory<T> : IMapperFactory<T> where T : class
    {
        private readonly Action<T, object> _emptyAction = (a, b) => { };
        
        public ITokenStreamMapper<T> CreateForMap(IMap<T> map)
        {
            var setters = new Action<T, object>[map.ColumnCount];

            for (var i = 0; i < map.ColumnCount; i++)
                setters[i] = GetDelegateForIndex(i);
            
            return new TokenStreamMapper<T>(setters);

            Action<T, object> GetDelegateForIndex(int index)
            {
                return map.HasDefinitionForColumn(index) ? GetPropSetter(map.GetTargetPropertyName(index)) : _emptyAction;
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