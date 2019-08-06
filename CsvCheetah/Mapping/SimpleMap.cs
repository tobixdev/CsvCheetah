using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace tobixdev.github.io.CsvCheetah.Mapping
{
    public class SimpleMap<T> : IMutableMap<T>
    {
        private readonly IDictionary<int, Expression<Func<T, string>>> _mappings;

        public SimpleMap()
        {
            _mappings = new Dictionary<int, Expression<Func<T, string>>>();
        }

        public int ColumnCount => _mappings.Count == 0 ? 0 : _mappings.Keys.Max() + 1;

        public void AddMapping(int column, Expression<Func<T, string>> propertyExpression)
        {
            _mappings[column] = propertyExpression;
        }
        
        public bool HasDefinitionForColumn(int columnIndex)
        {
            return _mappings.ContainsKey(columnIndex);
        }

        public Expression<Func<T, string>> GetPropertyExpression(int columnIndex)
        {
            if(!_mappings.ContainsKey(columnIndex))
                throw new MappingException($"No property for column {columnIndex} found.");

            return _mappings[columnIndex];
        }
    }
}