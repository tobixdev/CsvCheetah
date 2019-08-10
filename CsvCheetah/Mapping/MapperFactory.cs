using System;
using System.Linq.Expressions;
using System.Threading;

namespace tobixdev.github.io.CsvCheetah.Mapping
{
    public class MapperFactory<T> : IMapperFactory<T>
    {
        private readonly Action<T, object> _emptyAction = (a, b) => { };
        
        public ITokenStreamMapper<T> CreateForMap(IMap<T> map)
        {
            var setters = new Action<T, object>[map.ColumnCount];

            for (var i = 0; i < map.ColumnCount; i++)
            {
                if (map.HasDefinitionForColumn(i))
                {
                    setters[i] = GetPropSetter(map.GetTargetPropertyName(i));
                }
                else
                {
                    setters[i] = _emptyAction;
                }
            }
            
            return new TokenStreamMapper<T>(setters);
        }

        private static Action<T, object> GetPropSetter(string propertyName)
        {
            var property = typeof(T).GetProperty("propertyName");
            
            if(property == null)
                throw new MappingException($"Property {propertyName} does not exists on type {typeof(T)}.");

            var propertyType = property.PropertyType;
            
            var paramExpression = Expression.Parameter(typeof(T));
            var paramExpression2 = Expression.Parameter(propertyType, propertyName);

            var propertyGetterExpression = Expression.Property(paramExpression, propertyName);

            return Expression.Lambda<Action<T, object>>
            (
                Expression.Assign(propertyGetterExpression, paramExpression2), paramExpression, paramExpression2
            ).Compile();
        }
    }
}