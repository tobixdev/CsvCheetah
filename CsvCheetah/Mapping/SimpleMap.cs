using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace tobixdev.github.io.CsvCheetah.Mapping
{
    public class SimpleMap<T> : IMutableMap<T>
    {
        private readonly IDictionary<int, string> _mappings;

        public SimpleMap()
        {
            _mappings = new Dictionary<int, string>();
        }

        public int ColumnCount => _mappings.Count == 0 ? 0 : _mappings.Keys.Max() + 1;

        public void AddMapping(int column, string propertyName)
        {
            _mappings[column] = propertyName;
        }
        
        public bool HasDefinitionForColumn(int columnIndex)
        {
            return _mappings.ContainsKey(columnIndex);
        }

        public string GetTargetPropertyName(int columnIndex)
        {
            if(!_mappings.ContainsKey(columnIndex))
                throw new MappingException($"No property for column {columnIndex} found.");

            return _mappings[columnIndex];
        }
    }
}